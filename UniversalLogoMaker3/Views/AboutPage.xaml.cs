namespace UniversalLogoMaker3.Views
{
    using ViewModels;

    public sealed partial class AboutPage
    {
        public AboutViewModel ViewModel { get; } = new AboutViewModel();

        public AboutPage()
        {
            InitializeComponent();
            ViewModel.Initialize(webView);
        }
    }
}
