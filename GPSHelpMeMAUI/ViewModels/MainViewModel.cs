using GPSHelpMeMAUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSHelpMeMAUI.ViewModels
{
    internal class MainViewModel
    {
        private readonly ApiServices _apiService = new();

        public ObservableCollection<WeatherForecastModel> WeatherForecasts { get; set; } = new();

        public async Task LoadWeatherForecastsAsync()
        {
            var forecasts = await _apiService.GetForecastAsync();
            WeatherForecasts.Clear();
            foreach (var forecast in forecasts)
                WeatherForecasts.Add(forecast);
        }
    }
}
