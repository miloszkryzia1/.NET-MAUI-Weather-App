namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        SettingsPage settings;
        public MainViewModel ViewModel { get; set; }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            settings = new SettingsPage(BindingContext as MainViewModel);
            ViewModel = BindingContext as MainViewModel;
        }

        //First search
        private void InitialSearch(object sender, EventArgs e)
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
            locationButton.Clicked -= InitialSearch;
            searchBar.SearchButtonPressed -= InitialSearch;
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            Navigation.PushAsync(settings);
        }
    }
}