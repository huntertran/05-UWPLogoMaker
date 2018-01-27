namespace UniversalLogoMaker.Views
{
    using Windows.UI.Xaml.Navigation;
    using ViewModels;

    public sealed partial class SettingsPage
    {
        public SettingsViewModel ViewModel { get; } = new SettingsViewModel();

        //// TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere

        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Initialize();
        }
    }
}
