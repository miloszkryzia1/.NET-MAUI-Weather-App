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
        public float Temperature { get; set; }
        public string ImageSource { get; set; }
        public int Hour {  get; set; }

        public HourlyData(float temp, int hour, int conditionCode) 
        {
            Temperature = temp;
            Hour = hour;
            ImageSource = IconSelector.SelectImage(conditionCode);
        }
    }
}
