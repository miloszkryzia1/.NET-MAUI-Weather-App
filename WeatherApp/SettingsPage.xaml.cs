using System.Diagnostics;
using WeatherApp.Tools;

namespace WeatherApp;

public partial class SettingsPage : ContentPage
{
    MainViewModel mainVm;
    string cformat = "{0:F0}°C";
    string fformat = "{0:F0}°F";
	public SettingsPage(MainViewModel vm)
	{
        mainVm = vm;
		InitializeComponent();
	}
    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            mainVm.CurrentDisplayedTemp = string.Format(cformat, mainVm.CurrentTempC);
            mainVm.CurrentDisplayedWind = string.Format("{0:F1} km/h", mainVm.CurrentWindKph);
            mainVm.CurrentDisplayedPrec = string.Format("{0} mm", mainVm.CurrentPrecipitationMm);
            mainVm.Units = "metric";
            foreach (var hour in mainVm.Hourly)
            {
                hour.DisplayedTemperature = string.Format(cformat, hour.TemperatureC);
            }
            foreach (var day in mainVm.Daily)
            {
                day.DisplayedMinTemp = string.Format(cformat, day.MinTempC);
                day.DisplayedMaxTemp = string.Format(cformat, day.MaxTempC);
            }
        }
        else
        {
            mainVm.CurrentDisplayedTemp = string.Format(fformat, mainVm.CurrentTempF);
            mainVm.CurrentDisplayedWind = string.Format("{0:F1} mph", mainVm.CurrentWindMph);
            mainVm.CurrentDisplayedPrec = string.Format("{0:F2} in", mainVm.CurrentPrecipitationIn);
            mainVm.Units = "imperial";
            foreach (var hour in mainVm.Hourly)
            {
                hour.DisplayedTemperature = string.Format(fformat, hour.TemperatureF);
            }
            foreach (var day in mainVm.Daily)
            {
                day.DisplayedMinTemp = string.Format(fformat, day.MinTempF);
                day.DisplayedMaxTemp = string.Format(fformat, day.MaxTempF);
            }
        }
    }
}