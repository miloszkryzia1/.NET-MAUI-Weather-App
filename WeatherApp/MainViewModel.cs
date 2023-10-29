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
using WeatherApp.Tools;
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
        public float CurrentWind { get; set; }
        public float CurrentPrecipitation { get; set; }
        public ObservableCollection<HourlyData> Hourly {  get; set; }
        public ObservableCollection<DailyData> Daily { get; set; }
        public bool DoneLoading { get; set; }

        HttpClient client;
        string baseUrl;
        string key;
        public MainViewModel()
        {
            client = new HttpClient();
            baseUrl = "https://api.weatherapi.com/v1";
            key = "37685754eee8462993275728232410";
            Hourly = new ObservableCollection<HourlyData>();
            Daily = new ObservableCollection<DailyData>();
        }

        public ICommand UpdateWeatherCommand => new Command(async (searchTerm) =>
        {
            DoneLoading = false;
            Hourly.Clear();
            Daily.Clear();

            //get week data
            var weekUrl = $"{baseUrl}/forecast.json?key={key}&q={(string)searchTerm}&days=7";
            var weekResponse = await client.GetAsync(weekUrl);
            using var weekStream = await weekResponse.Content.ReadAsStreamAsync();
            var weekData = await JsonSerializer.DeserializeAsync<ForecastData>(weekStream);
            
            //assign properties
            CurrentCity = weekData.location.name;
            CurrentTemp = weekData.current.temp_c;
            CurrentImage = IconSelector.SelectImage(weekData.current.condition.code);
            CurrentWind = weekData.current.wind_kph;
            CurrentPrecipitation = weekData.current.precip_mm;

            //Today's hourly
            var hourlyToday = weekData.forecast.forecastday[0].hour;
            var hourlyTomorrow = weekData.forecast.forecastday[1].hour;
            var time = weekData.location.localtime_epoch;
            var utcTime = DateTimeOffset.FromUnixTimeSeconds(time).DateTime;
            var timeZoneId = weekData.location.tz_id;
            var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            var hour = TimeZoneInfo.ConvertTimeFromUtc(utcTime, targetTimeZone).Hour;

            var currentArray = hourlyToday;
            var i = hour + 1;
            for (int j = 0; j < 12; j++)
            {
                if (i > 23)
                {
                    i = 0;
                    currentArray = hourlyTomorrow;
                }
                Hourly.Add(new HourlyData(currentArray[i].temp_c, i, currentArray[i].condition.code));
                i++;
            }

            //Next 6 days
            for (int j = 1; j < 7; j++)
            {
                var date = weekData.forecast.forecastday[j].date;
                var day = weekData.forecast.forecastday[j].day;
                Daily.Add(new DailyData(day.mintemp_c, day.maxtemp_c, date, day.condition.code));
            }

            DoneLoading = true;
        });
    }
}
