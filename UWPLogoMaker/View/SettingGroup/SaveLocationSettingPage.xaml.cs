using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using UWPLogoMaker.Utilities;
using UWPLogoMaker.ViewModel.SettingGroup;

namespace UWPLogoMaker.View.SettingGroup
{
    public sealed partial class SaveLocationSettingPage
    {

        private readonly SaveLocationSettingViewModel _vm;

        public SaveLocationSettingPage()
        {
            InitializeComponent();
            Loaded += SettingPage_Loaded;
            _vm = DataContext as SaveLocationSettingViewModel;
        }

        private void SettingPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int saveMode = SettingManager.GetSaveMode();
            if (saveMode == 0 || saveMode == 1)
            {
                //default
                saveMode = 2;
                SettingManager.SetSaveMode(2);
            }
            switch (saveMode)
            {
                case 1:
                    break;
                case 2:
                    SaveMode2RadioButton.IsChecked = true;
                    break;
                case 3:
                    SaveMode3RadioButton.IsChecked = true;
                    break;
            }
        }

        private async void BrowseButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            await _vm.BrowseToSaveFolder();
        }

        private async void RadioButton_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            Debug.Assert(rb != null, "rb != null");
            string mode = rb.Tag.ToString();
            switch (mode)
            {
                case "1":
                    //Save in the same folder
                    SettingManager.SetSaveMode(1);
                    break;
                case "2":
                    //Choose where to save
                    SettingManager.SetSaveMode(2);
                    break;
                case "3":
                    //Save in specific folder
                    
                    if (string.IsNullOrEmpty(SaveFolderPath.Text))
                    {
                        bool isSuccess = await _vm.BrowseToSaveFolder();
                        if (isSuccess)
                        {
                            SettingManager.SetSaveMode(3);
                        }
                        else
                        {
                            SettingManager.SetSaveMode(2);
                            SaveMode2RadioButton.IsChecked = true;
                        }
                    }
                    break;
            }
        }

        private void RadioButton_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }
    }
}