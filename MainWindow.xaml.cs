using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace WeatherApp
{
    public partial class MainWindow : Window
    {
        private readonly string apiKey = Environment.GetEnvironmentVariable("WeatherApp_ApiKey")
            ?? throw new InvalidOperationException("Missing env var WeatherApp_ApiKey");

        private readonly string requestUrl = "https://api.openweathermap.org/data/2.5/weather";

        public MainWindow()
        {
            InitializeComponent();

            // Kick off initial load 
            _ = LoadInitialAsync();
        }

        private async Task LoadInitialAsync()
        {
            try
            {
                var result = await GetWeatherDataAsync("Augsburg");
                UpdateUi(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load initial weather: {ex.Message}");
            }
        }

        public void UpdateUi(string city) => _ = UpdateUiAsync(city);
        public void UpdateUi(int zip) => _ = UpdateUiAsync(zip);

        private void UpdateUi(OpenWeatherResponse result)
        {
            string defaultImage = "Sun.png";
            string defaultIcon = "Sunicon.png";
            string currentWeather = result.weather?.FirstOrDefault()?.Main?
                .ToLowerInvariant() ?? string.Empty;

            if (currentWeather.Contains("cloud"))
            {
                defaultImage = "Cloud.png";
                defaultIcon = "Cloudicon.png";
            }
            else if (currentWeather.Contains("rain"))
            {
                defaultImage = "Rain.png";
                defaultIcon = "Rainicon.png";
            }
            else if (currentWeather.Contains("snow"))
            {
                defaultImage = "Snow.png";
                defaultIcon = "Snowicon.png";
            }

            mainBackground.ImageSource =
                new BitmapImage(new Uri($"pack://application:,,,/Images/{defaultImage}", UriKind.Absolute));
            conditionIcon.Source =
                new BitmapImage(new Uri($"pack://application:,,,/Images/{defaultIcon}", UriKind.Absolute));

            labelCity.Content = string.IsNullOrWhiteSpace(result.Name) ? "—" : result.Name;
            labelTemp.Content = $"{result.main.Temp:F1}°C";
            labelDescription.Content = currentWeather;

            labelFeelsLike.Content = $"{result.main.Feels_Like:F1}°C";
            labelMinMax.Content = $"{result.main.Temp_Min:F0}°C / {result.main.Temp_Max:F0}°C";
            labelHumidity.Content = $"{result.main.Humidity:F0}%";
        }

        private async Task UpdateUiAsync(string city)
        {
            try
            {
                var result = await GetWeatherDataAsync(city);
                UpdateUi(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching weather: {ex.Message}");
            }
        }

        private async Task UpdateUiAsync(int zip)
        {
            try
            {
                var result = await GetWeatherDataAsync(zip);
                UpdateUi(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching weather: {ex.Message}");
            }
        }

        public async Task<OpenWeatherResponse> GetWeatherDataAsync(string city)
        {
            using var httpClient = new HttpClient();

            string callUri = $"{requestUrl}?q={Uri.EscapeDataString(city)}&appid={apiKey}&units=metric";
            HttpResponseMessage httpResponse = await httpClient.GetAsync(callUri);

            if (!httpResponse.IsSuccessStatusCode)
                throw new InvalidOperationException($"API returned {httpResponse.StatusCode}");

            string response = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OpenWeatherResponse>(response)!;
        }

        public async Task<OpenWeatherResponse> GetWeatherDataAsync(int zip) // overload for zip request
        {
            using var httpClient = new HttpClient();

            string callUri = $"{requestUrl}?zip={zip},DE&appid={apiKey}&units=metric";
            HttpResponseMessage httpResponse = await httpClient.GetAsync(callUri);

            if (!httpResponse.IsSuccessStatusCode)
                throw new InvalidOperationException($"API returned {httpResponse.StatusCode}");

            string response = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OpenWeatherResponse>(response)!;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var query = searchBox.Text?.Trim();
            if (string.IsNullOrWhiteSpace(query))
                return;

            if (int.TryParse(query, out var zip))
                await UpdateUiAsync(zip);
            else
                await UpdateUiAsync(query!);
        }
    }
}