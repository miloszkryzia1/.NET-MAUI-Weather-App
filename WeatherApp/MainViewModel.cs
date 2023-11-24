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
        public float CurrentTempC { get; set; }
        public float CurrentTempF { get; set; }
        public string CurrentDisplayedTemp {  get; set; }
        public string CurrentImage { get; set; }
        public string CurrentCity { get; set; }
        public float CurrentWindKph { get; set; }
        public float CurrentWindMph { get; set; }
        public string CurrentDisplayedWind { get; set; }
        public float CurrentPrecipitationMm { get; set; }
        public float CurrentPrecipitationIn { get; set; }
        public string CurrentDisplayedPrec {  get; set; }
        public ObservableCollection<HourlyData> Hourly {  get; set; }
        public ObservableCollection<DailyData> Daily { get; set; }
        public bool DoneLoading { get; set; }
        public string Units { get; set; }
        public Color PrimaryColor { get; set; }
        public Color SecondaryColor { get; set;}
        public Color TextColor { get; set; }

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
            Units = "metric";
            PrimaryColor = Color.Parse("#f5f5f5");
            SecondaryColor = Color.Parse("#e6e6e6");
            TextColor = Color.Parse("Black");
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

            if (weekData != null)
            {
                //assign properties
                CurrentCity = weekData.location.name;
                CurrentTempC = weekData.current.temp_c;
                CurrentTempF = weekData.current.temp_f;
                CurrentImage = IconSelector.SelectImage(weekData.current.condition.code, weekData.current.is_day);
                CurrentWindKph = weekData.current.wind_kph;
                CurrentWindMph = weekData.current.wind_mph;
                CurrentPrecipitationMm = weekData.current.precip_mm;
                CurrentPrecipitationIn = weekData.current.precip_in;

                if (Units == "metric")
                {
                    CurrentDisplayedTemp = string.Format("{0:F0}°C", CurrentTempC);
                    CurrentDisplayedWind = string.Format("{0:F1} km/h", CurrentWindKph);
                    CurrentDisplayedPrec = string.Format("{0} mm", CurrentPrecipitationMm);
                }
                else
                {
                    CurrentDisplayedTemp = string.Format("{0:F0}°F", CurrentTempF);
                    CurrentDisplayedWind = string.Format("{0:F1} mph", CurrentWindMph);
                    CurrentDisplayedPrec = string.Format("{0:F2} in", CurrentPrecipitationIn);
                }

                if (weekData.current.is_day == 1)
                {
                    PrimaryColor = Color.Parse("#f5f5f5");
                    SecondaryColor = Color.Parse("#e6e6e6");
                    TextColor = Color.Parse("Black");
                }
                else
                {
                    PrimaryColor = Color.Parse("#2a2f42");
                    SecondaryColor = Color.Parse("#444d6e");
                    TextColor = Color.Parse("White");
                }

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
                    Hourly.Add(new HourlyData(currentArray[i].temp_c, currentArray[i].temp_f, i, currentArray[i].condition.code, Units, currentArray[i].is_day));
                    i++;
                }

                //Next 6 days
                for (int j = 1; j < 3; j++)
                {
                    var date = weekData.forecast.forecastday[j].date;
                    var day = weekData.forecast.forecastday[j].day;
                    Daily.Add(new DailyData(day.mintemp_c, day.maxtemp_c, day.mintemp_f, day.maxtemp_f, date, day.condition.code, Units));
                }

                DoneLoading = true;
            }
        });
    }
}
