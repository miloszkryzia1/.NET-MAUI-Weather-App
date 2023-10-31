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
        Stopwatch sw = Stopwatch.StartNew();
        if (e.Value)
        {
            mainVm.CurrentDisplayedTemp = string.Format("{0:F0}°C", mainVm.CurrentTempC);
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
        sw.Stop();
        Debug.WriteLine(sw.ElapsedMilliseconds + " ms");
    }
}