namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        SettingsPage settings;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            settings = new SettingsPage(BindingContext as MainViewModel);
        }

        //First search
        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            welcomeScreen.IsVisible = false;
            var vm = BindingContext as MainViewModel;
            Binding binding = new Binding();
            binding.Source = vm;
            binding.Path = "DoneLoading";
            DataTrigger trigger = new DataTrigger(typeof(ActivityIndicator));
            trigger.Binding = binding;
            trigger.Value = false;
            Setter setter = new Setter();
            setter.Property = ActivityIndicator.IsRunningProperty;
            setter.Value = true;
            trigger.Setters.Add(setter);
            indicator.Triggers.Add(trigger);
            ((SearchBar)sender).SearchButtonPressed -= SearchBar_SearchButtonPressed;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(settings);
        }
    }
}