using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
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

        private async void SettingPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await SetDefaultSaveMode();
        }

        private async Task SetDefaultSaveMode()
        {
            SaveMode saveMode = await SettingManager.GetSaveMode();

            if (saveMode == SaveMode.None || saveMode == SaveMode.SameFolder)
            {
                //default
                saveMode = SaveMode.UserChooseToSave;
                SettingManager.SetSaveMode(saveMode);
            }

            switch (saveMode)
            {
                case SaveMode.UserChooseToSave:
                    SaveMode2RadioButton.IsChecked = true;
                    break;
                case SaveMode.SpecificFoler:
                    SaveMode3RadioButton.IsChecked = true;
                    break;
            }
        }

        private async void RadioButton_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null)
            {
                string mode = rb.Tag.ToString();

                switch (mode)
                {
                    case "1":
                        //Save in the same folder
                        SettingManager.SetSaveMode(SaveMode.SameFolder);
                        break;
                    case "2":
                        //Choose where to save
                        SettingManager.SetSaveMode(SaveMode.UserChooseToSave);
                        break;
                    case "3":
                        //Save in specific folder
                        if (string.IsNullOrEmpty(SaveFolderPath.Text))
                        {
                            bool isSuccess = await _vm.BrowseToSaveFolder();
                            if (isSuccess)
                            {
                                SettingManager.SetSaveMode(SaveMode.SpecificFoler);
                            }
                            else
                            {
                                SettingManager.SetSaveMode(SaveMode.UserChooseToSave);
                                SaveMode2RadioButton.IsChecked = true;
                            }
                        }
                        break;
                }
            }
        }

        private void RadioButton_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }
    }
}