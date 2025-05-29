using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GPSHelpMeMAUI.Services
{
    internal class ApiServices
    {
        private readonly HttpClient _client;

        public ApiServices()
        {
            _client = new HttpClient
            {
                //BaseAddress = new Uri("https://10.0.2.2:5001") // Emulator accessing host machine
                //Beware:  Emulators no longer allow http.
                //BaseAddress = new Uri("http://10.0.2.2:5268")
                //BaseAddress = new Uri("http://10.0.2.2:7086")
                BaseAddress = new Uri("https://mqjyf3huh5.us-east-2.awsapprunner.com") // AWS App Runner
            };
        }

        public async Task<List<WeatherForecastModel>> GetForecastAsync()
        {
            try
            {
                //using var response = await _client.GetAsync("api/weatherforecast/forecast");
                using var response = await _client.GetAsync("weatherforecast");//ToDo: This is from a previous deployment, so it only uses "weatherforecast". Once it's redeployed, use "api/weatherforecast/forecast".

                if (!response.IsSuccessStatusCode) // if error
                {
                    // You can log this or handle it as needed
                    return new List<WeatherForecastModel>();
                }

                var json = await response.Content.ReadAsStringAsync();

                var data = JsonSerializer.Deserialize<List<WeatherForecastModel>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<WeatherForecastModel>();
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                Console.WriteLine($"Error fetching forecast: {ex.Message}");
                return new List<WeatherForecastModel>();
            }
        }

    }

    public class WeatherForecastModel
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
