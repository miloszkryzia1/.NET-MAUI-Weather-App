using System.Diagnostics;

namespace WeatherApp;

public partial class SettingsPage : ContentPage
{
	ContentPage mainPage;
	public SettingsPage(ContentPage main)
	{
		mainPage = main;
		InitializeComponent();
	}

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var hourly = (CollectionView)mainPage.FindByName("hourlyCollection");
        var daily = (CollectionView)mainPage.FindByName("dailyCollection");
        var currentTempLabel = (Label)mainPage.FindByName("currentTempLabel");
        if (e.Value)
        {
            Application.Current.Resources.TryGetValue("celsiusTemplate", out var hourTemplate);
            hourly.ItemTemplate = (DataTemplate)hourTemplate;
            Application.Current.Resources.TryGetValue("celsiusDayTemplate", out var dayTemplate);
            daily.ItemTemplate = (DataTemplate)dayTemplate;
            currentTempLabel.SetBinding(Label.TextProperty, new Binding()
            {
                Source = mainPage.BindingContext,
                Path = "CurrentTempC",
                StringFormat = "{0:F0}°C"
            });
        }
        else
        {
            Application.Current.Resources.TryGetValue("fahrenheitTemplate", out var hourTemplate);
            hourly.ItemTemplate = (DataTemplate)hourTemplate;
            Application.Current.Resources.TryGetValue("fahrenheitDayTemplate", out var dayTemplate);
            daily.ItemTemplate = (DataTemplate)dayTemplate;
            currentTempLabel.SetBinding(Label.TextProperty, new Binding()
            {
                Source = mainPage.BindingContext,
                Path = "CurrentTempF",
                StringFormat = "{0:F0}°F"
            });
        }
    }
}