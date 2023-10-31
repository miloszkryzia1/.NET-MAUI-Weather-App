using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Tools;

namespace WeatherApp.Models
{
    [AddINotifyPropertyChangedInterface]
    public class HourlyData
    {
        public float TemperatureC { get; set; }
        public float TemperatureF { get; set; }
        public string DisplayedTemperature {  get; set; }
        public string ImageSource { get; set; }
        public int Hour {  get; set; }

        public HourlyData(float tempC, float tempF, int hour, int conditionCode, string units) 
        {
            TemperatureC = tempC;
            TemperatureF = tempF;
            if (units == "metric")
            {
                DisplayedTemperature = string.Format("{0:F0}°C", TemperatureC);
            }
            else
            {
                DisplayedTemperature = string.Format("{0:F0}°F", TemperatureF);
            }
            Hour = hour;
            ImageSource = IconSelector.SelectImage(conditionCode);
        }
    }
}
