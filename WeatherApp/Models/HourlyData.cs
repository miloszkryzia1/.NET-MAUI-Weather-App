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

        public HourlyData(float temp, int conditionCode) 
        {
            Temperature = temp;
            ImageSource = IconSelector.SelectImage(conditionCode);
        }
    }
}
