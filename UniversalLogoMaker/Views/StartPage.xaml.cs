namespace UniversalLogoMaker.Views
{
    using Utilities;
    using ViewModels;

    public sealed partial class StartPage
    {
        public StartViewModel Vm => (StartViewModel)DataContext;

        public StartPage()
        {
            InitializeComponent();
        }

        private async void StartPage_Loaded()
        {
            await Vm.Initialize();
            await ApiService.UpdateDatabase();

            //// If you want to replace windows title bar with application own title bar
            //CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            //coreTitleBar.ExtendViewIntoTitleBar = true;
        }
    }
}
