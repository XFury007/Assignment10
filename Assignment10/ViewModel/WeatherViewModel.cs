using Assignment10.Models;
using Assignment10.Services;
using System.Runtime.CompilerServices;  
using System.Windows.Input;
using System.ComponentModel;


namespace Assignment10.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private readonly WeatherService _weatherService;
        private Root _weatherData;
        private string _cityName;
        private bool _isLoading;
       
       

        // Event from INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand FetchWeatherCommand { get; }
        public ICommand UseMyLocationCommand { get; }

       
        public WeatherViewModel()
        {
            _weatherService = new WeatherService();
            FetchWeatherCommand = new Command(async () => await GetWeather());
            UseMyLocationCommand = new Command(async () => await GetWeatherByLocation());

        }

        // City name bound to UI
        public string CityName
        {
            get => _cityName;
            set
            {
                if (_cityName != value)
                {
                    _cityName = value;
                    OnPropertyChanged();
                }
            }
        }

        // Shows loading state
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        // Holds the weather result
        public Root WeatherData
        {
            get => _weatherData;
            set
            {
                if (_weatherData != value)
                {
                    _weatherData = value;
                    OnPropertyChanged();
                }
            }
        }

        
        
        private async Task GetWeather()
        {
            if (string.IsNullOrWhiteSpace(CityName))
                return;

           

            string apiKey = "d8bf5a6a17a401d34db04ab322fc7248";

            // Fetch weather data
            IsLoading = true;
            WeatherData = await _weatherService.GetWeatherAsync(CityName, apiKey);

            IsLoading = false;
        }

        private async Task GetWeatherByLocation()
        {
            string apiKey = "d8bf5a6a17a401d34db04ab322fc7248"; 
            IsLoading = true;

            var location = await GeolocationServices.GetCurrentLocationAsync();
            if (location != null)
            {
                WeatherData = await _weatherService.GetWeatherByCoordsAsync(location.Latitude, location.Longitude, apiKey);
            }

            IsLoading = false;
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
