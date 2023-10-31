using System.Diagnostics;
using WeatherApp.Tools;

namespace WeatherApp;

public partial class SettingsPage : ContentPage
{
    MainViewModel mainVm;
	public SettingsPage(MainViewModel vm)
	{
        mainVm = vm;
		InitializeComponent();
	}
    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            mainVm.CurrentDisplayedTemp = string.Format("{0:F0}°C", mainVm.CurrentTempC);
            mainVm.CurrentDisplayedWind = string.Format("{0:F1} km/h", mainVm.CurrentWindKph);
            mainVm.CurrentDisplayedPrec = string.Format("{0} mm", mainVm.CurrentPrecipitationMm);
            mainVm.Units = "metric";
            foreach (var hour in mainVm.Hourly)
            {
                hour.DisplayedTemperature = string.Format("{0:F0}°C", hour.TemperatureC);
            }
            foreach (var day in mainVm.Daily)
            {
                day.DisplayedMinTemp = string.Format("{0:F0}°C", day.MinTempC);
                day.DisplayedMaxTemp = string.Format("{0:F0}°C", day.MaxTempC);
            }
        }
        else
        {
            mainVm.CurrentDisplayedTemp = string.Format("{0:F0}°F", mainVm.CurrentTempF);
            mainVm.CurrentDisplayedWind = string.Format("{0:F1} mph", mainVm.CurrentWindMph);
            mainVm.CurrentDisplayedPrec = string.Format("{0:F2} in", mainVm.CurrentPrecipitationIn);
            mainVm.Units = "imperial";
            foreach (var hour in mainVm.Hourly)
            {
                hour.DisplayedTemperature = string.Format("{0:F0}°F", hour.TemperatureF);
            }
            foreach (var day in mainVm.Daily)
            {
                day.DisplayedMinTemp = string.Format("{0:F0}°F", day.MinTempF);
                day.DisplayedMaxTemp = string.Format("{0:F0}°F", day.MaxTempF);
            }
        }
    }
}