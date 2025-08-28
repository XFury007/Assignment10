using Assignment10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json; 
using Assignment10.Models;

namespace Assignment10.Services
{
   public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Root> GetWeatherAsync(string cityName, string apiKey)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=metric";
            
            var response = await _httpClient.GetAsync(url);

            if(!response.IsSuccessStatusCode)
             return null;

            string json = await response.Content.ReadAsStringAsync();

            var weatherData = JsonSerializer.Deserialize<Root>(json);

            return weatherData;

        }
    }
}
