using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Tools;

namespace WeatherApp.Models
{
    public class HourlyData
    {
        public float TemperatureC { get; set; }
        public float TemperatureF { get; set; }
        public string ImageSource { get; set; }
        public int Hour {  get; set; }

        public HourlyData(float tempC, float tempF, int hour, int conditionCode) 
        {
            TemperatureC = tempC;
            TemperatureF = tempF;
            Hour = hour;
            ImageSource = IconSelector.SelectImage(conditionCode);
        }
    }
}
