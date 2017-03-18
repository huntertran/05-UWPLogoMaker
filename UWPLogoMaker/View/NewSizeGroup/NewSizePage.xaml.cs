namespace UWPLogoMaker.View.NewSizeGroup
{
    using System;
    using Windows.UI.Popups;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Model;
    using Utilities;
    using ViewModel;
    using ViewModel.NewSizeGroup;

    public sealed partial class NewSizePage
    {
        public readonly NewSizeViewModel Vm = new NewSizeViewModel();

        public NewSizePage()
        {
            InitializeComponent();
            Loaded += NewSizePage_Loaded;
        }

        private async void NewSizePage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await Vm.LoadData();
        }

        private async void AddButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PlatformNameTextBox.Text) || string.IsNullOrEmpty(PlatformShortNameTextBox.Text))
            {
                MessageDialog msg = new MessageDialog("Please enter platform name and shortname", "Error");
                await msg.ShowAsync();
            }
            else
            {
                await Vm.AddNewPlatform(PlatformNameTextBox.Text, PlatformShortNameTextBox.Text, SizeTextBox.Text);
            }
        }

        private async void DeletedPlatform_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Platform p = ((Button)sender).Tag as Platform;
            StaticData.StartVm.CustomData.PlatformList.Remove(p);
            await StorageHelper.Object2Json(StaticData.StartVm.CustomData, "custom.dat");
        }

        private void SizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Vm.ParseLogoObject(SizeTextBox.Text);
        }
    }
}
