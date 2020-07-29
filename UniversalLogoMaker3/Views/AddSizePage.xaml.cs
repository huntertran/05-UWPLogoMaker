namespace UniversalLogoMaker3.Views
{
    using Models;
    using Services;
    using ViewModels;
    using Windows.UI.Popups;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

    public sealed partial class AddSizePage
    {
        public AddSizeViewModel ViewModel { get; } = new AddSizeViewModel();

        public AddSizePage()
        {
            InitializeComponent();
            Loaded += AddSizePage_Loaded;
        }

        private async void AddSizePage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.LoadData();
        }

        private async void DeletedPlatform_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Platform p = ((Button)sender).Tag as Platform;
            ViewModel.CustomData.PlatformList.Remove(p);
            await StorageService.Object2Json(ViewModel.CustomData, "custom.dat");
        }

        private async void AddButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PlatformNameTextBox.Text) || string.IsNullOrEmpty(PlatformShortNameTextBox.Text))
            {
                MessageDialog msg = new MessageDialog("Please enter platform name and shortname", "Error");
                msg.ShowAsync();
            }
            else
            {
                await ViewModel.AddNewPlatform(PlatformNameTextBox.Text, PlatformShortNameTextBox.Text, SizeTextBox.Text);
            }
        }

        private void SizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.ParseLogoObject(SizeTextBox.Text);
        }
    }
}
