using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<HourlyData> Hourly {  get; set; }
        //public ObservableCollection<WeatherData> Daily { get; set; }

        HttpClient client;
        string baseUrl;
        string key;
        public MainViewModel()
        {
            client = new HttpClient();
            baseUrl = "https://api.weatherapi.com/v1";
            key = "37685754eee8462993275728232410";
            Hourly = new ObservableCollection<HourlyData>();
            //Daily = new ObservableCollection<WeatherData>(); //Separate daily class needed
        }

        public ICommand UpdateWeatherCommand => new Command(async (searchTerm) =>
        {
            //FIXME - NEED HOURLY FORECAST STARTING TODAY
            //get Today's data
            var todayUrl = $"{baseUrl}/forecast.json?key={key}&q={(string)searchTerm}&dt={DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Year}";
            var todayResponse = await client.GetAsync(todayUrl);
            using var todayStream = await todayResponse.Content.ReadAsStreamAsync();
            var todayData = await JsonSerializer.DeserializeAsync<ForecastData>(todayStream);
            
            //get week data
            var weekUrl = $"{baseUrl}/forecast.json?key={key}&q={(string)searchTerm}&days=7";
            var weekResponse = await client.GetAsync(weekUrl);
            using var weekStream = await weekResponse.Content.ReadAsStreamAsync();
            var weekData = await JsonSerializer.DeserializeAsync<ForecastData>(weekStream);
            
            //assign parameters and lists
            CurrentCity = todayData.location.name;
            CurrentTemp = todayData.current.temp_c;

            //Today's hourly
            var hourlyToday = todayData.forecast.forecastday[0].hour;
            var hourlyTomorrow = weekData.forecast.forecastday[0].hour;
            var time = todayData.location.localtime_epoch;
            var utcTime = DateTimeOffset.FromUnixTimeSeconds(time).DateTime;
            var timeZoneId = todayData.location.tz_id;
            var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            var hour = TimeZoneInfo.ConvertTimeFromUtc(utcTime, targetTimeZone).Hour;

            var currentArray = hourlyToday;
            var i = 0;
            for (int j = 0; i < 7; j++)
            {
                if (i > 23)
                {
                    i = 0;
                    currentArray = hourlyTomorrow;
                }
                Hourly.Add(new HourlyData(currentArray[i].temp_c, currentArray[i].condition.code));
                i++;
            }
        });
    }
}
