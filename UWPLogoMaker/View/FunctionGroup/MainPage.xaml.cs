namespace UWPLogoMaker.View.FunctionGroup
{
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Model;
    using ViewModel.FunctionGroup;
    using ViewModel.FunctionGroup.BackgroundGroup;

    public sealed partial class MainPage
    {
        public MainViewModel Vm => (MainViewModel)DataContext;

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            PreviewFrame.Navigate(typeof (PreviewPage));
            MainPlatformListView.SelectedIndex = 0;
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pivot p = sender as Pivot;
            if (p?.SelectedIndex != -1)
            {
                var selectedBackgroundMode = p?.SelectedItem as AvailableBackgroundMode;
                if (selectedBackgroundMode != null)
                {
                    BackgroundFrame.Navigate(selectedBackgroundMode.ClassToNavigate);
                }
            }
        }

        private async void GenerateLogo_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Vm.IsShowingProgress = true;
            await Vm.DoTheGenerateWin2DTask();
        }

        private void MainPlatformListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Platform platform in MainPlatformListView.SelectedItems)
            {
                platform.IsEnabled = true;
            }
        }

        private void CustomePlatformListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Platform platform in CustomePlatformListView.SelectedItems)
            {
                platform.IsEnabled = true;
            }
        }

    }
}
