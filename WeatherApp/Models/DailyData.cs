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
        public string DisplayedMinTemp { get; set; }
        public string DisplayedMaxTemp { get; set; }
        public string ImageSource { get; set; }
        public string Date { get; set; }
        public DailyData(float minTempC, float maxTempC, float minTempF, float maxTempF, string date, int conditionCode, string units) 
        {
            MinTempC = minTempC; 
            MaxTempC = maxTempC;
            MinTempF = minTempF;
            MaxTempF = maxTempF;
            if (units == "metric")
            {
                DisplayedMinTemp = string.Format("{0:F0}°C", minTempC);
                DisplayedMaxTemp = string.Format("{0:F0}°C", maxTempC);
            }
            else
            {
                DisplayedMinTemp = string.Format("{0:F0}°F", minTempF);
                DisplayedMaxTemp = string.Format("{0:F0}°F", maxTempF);
            }
            Date = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).ToString("ddd d");
            ImageSource = IconSelector.SelectImage(conditionCode);
        }
    }
}
