namespace UniversalLogoMaker.Views
{
    using System.Threading.Tasks;
    using Utilities;
    using ViewModels;
    using Windows.UI.Xaml;

    public sealed partial class StartPage
    {
        public StartViewModel Vm => (StartViewModel)DataContext;

        public StartPage()
        {
            InitializeComponent();
        }

        private async void StartPage_Loaded(object sender, RoutedEventArgs e)
        {
            await Vm.Initialize();
            await Task.Run(ApiService.UpdateDatabase);

            //// If you want to replace windows title bar with application own title bar
            //CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            //coreTitleBar.ExtendViewIntoTitleBar = true;
        }
    }
}
