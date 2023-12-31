﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Tools
{
    public class IconSelector
    {
        public static string SelectImage(int conditionCode, int day)
        {
            switch (conditionCode)
            {
                case 1000:
                    if (day == 1)
                    {
                        return "sun.png";
                    }
                    else
                    {
                        return "cloudy_night.png";
                    }
                case 1003:
                    if (day == 1)
                    {
                        return "cloudy.png";
                    }
                    else
                    {
                        return "cloudy_night.png";
                    }
                case 1006:
                case 1009:
                    return "clouds.png";
                case 1030:
                case 1135:
                    return "foggy.png";
                case 1063:
                case 1072:
                case 1150:
                case 1153:
                case 1168:
                case 1171:
                case 1180:
                case 1183:
                case 1186:
                case 1198:
                    return "drizzle.png";
                case 1189:
                case 1192:
                case 1195:
                case 1201:
                case 1240:
                case 1243:
                case 1246:
                    return "rain.png";
                case 1087:
                case 1273:
                case 1276:
                case 1279:
                case 1282:
                    return "storm.png";
                default:
                    return "snow.png";
            }
        }
    }
}
