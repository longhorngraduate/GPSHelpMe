using GPSHelpMeMAUI.ViewModels;

namespace GPSHelpMeMAUI
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel = new();
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            ForecastsList.ItemsSource = _viewModel.WeatherForecasts;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnLoadClicked(object sender, EventArgs e)
        {
            await _viewModel.LoadWeatherForecastsAsync();
        }
    }

}
