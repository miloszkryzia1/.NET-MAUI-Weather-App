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
        public float MinTempC {  get; set; }
        public float MaxTempC { get; set; }
        public float MinTempF { get; set; }
        public float MaxTempF { get; set; }
        public string ImageSource { get; set; }
        public string Date { get; set; }
        public DailyData(float minTempC, float maxTempC, float minTempF, float maxTempF, string date, int conditionCode) 
        {
            MinTempC = minTempC; 
            MaxTempC = maxTempC;
            MinTempF = minTempF;
            MaxTempF = maxTempF;
            Date = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).ToString("ddd d");
            ImageSource = IconSelector.SelectImage(conditionCode);
        }
    }
}
