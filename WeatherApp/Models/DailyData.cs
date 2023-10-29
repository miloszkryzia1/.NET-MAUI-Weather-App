using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Tools;

namespace WeatherApp.Models
{
    public class DailyData
    {
        public float MinTemp {  get; set; }
        public float MaxTemp { get; set; }
        public string ImageSource { get; set; }
        public string Date { get; set; }
        public DailyData(float minTemp, float maxTemp, string date, int conditionCode) 
        {
            MinTemp = minTemp; 
            MaxTemp = maxTemp;
            Date = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).ToString("ddd d");
            ImageSource = IconSelector.SelectImage(conditionCode);
        }
    }
}
