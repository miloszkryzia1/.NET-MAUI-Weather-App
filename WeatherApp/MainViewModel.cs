using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using static System.Net.WebRequestMethods;

namespace WeatherApp
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        public string CurrentStatus {  get; set; }
        public float CurrentTemp { get; set; }
        public string CurrentImage { get; set; }
        public string CurrentCity { get; set; }
        HttpClient client;
        string baseUrl;
        string key;
        public MainViewModel()
        {
            client = new HttpClient();
            baseUrl = "https://api.weatherapi.com/v1";
            key = "37685754eee8462993275728232410";
        }

        public ICommand UpdateWeatherCommand => new Command(async () =>
        {
            var url = $"{baseUrl}/current.json?key={key}&q=Paris";
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var data = await JsonSerializer.DeserializeAsync<WeatherData>(stream);
                CurrentStatus = data.current.condition.text;
                CurrentCity = data.location.name;
                CurrentTemp = data.current.temp_c;
            }
        });
    }
}
