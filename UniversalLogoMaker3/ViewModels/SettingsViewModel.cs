namespace UniversalLogoMaker3.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Helpers;
    using Infrastructure;
    using Services;
    using Services.Setting;
    using Windows.ApplicationModel;
    using Windows.Storage.AccessCache;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml;

    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/pages/settings.md
    public class SettingsViewModel : Observable
    {
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;
        private string _saveFolderPath;
        private ICommand _saveLocationCommand;
        private SaveLocationMode _saveLocationMode = GetSaveMode();
        private ICommand _switchThemeCommand;
        private string _versionDescription;

        public ElementTheme ElementTheme
        {
            get => _elementTheme;
            set => Set(ref _elementTheme, value);
        }

        public string SaveFolderPath
        {
            get => _saveFolderPath;
            set => Set(ref _saveFolderPath, value);
        }

        public ICommand SaveLocationCommand
        {
            get
            {
                if (_saveLocationCommand != null)
                {
                    return _saveLocationCommand;
                }

                return _saveLocationCommand = new RelayCommand<SaveLocationMode>(ChangeSaveMode);
            }
        }

        public SaveLocationMode SaveLocationMode
        {
            get => _saveLocationMode;
            set => Set(ref _saveLocationMode, value);
        }

        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand != null)
                {
                    return _switchThemeCommand;
                }

                return _switchThemeCommand = new RelayCommand<ElementTheme>(ChangeTheme);
            }
        }

        public string VersionDescription
        {
            get => _versionDescription;
            set => Set(ref _versionDescription, value);
        }

        public async Task<bool> BrowseToSaveFolder()
        {
            FolderPicker fPicker = new FolderPicker {SuggestedStartLocation = PickerLocationId.PicturesLibrary};
            fPicker.FileTypeFilter.Add(".jpeg");
            fPicker.FileTypeFilter.Add(".jpg");
            fPicker.FileTypeFilter.Add(".png");
            fPicker.FileTypeFilter.Add(".bmp");
            fPicker.FileTypeFilter.Add(".tiff");
            fPicker.FileTypeFilter.Add(".gif");

            StaticData.SaveFolder = await fPicker.PickSingleFolderAsync();
            if (StaticData.SaveFolder != null)
            {
                SaveFolderPath = StaticData.SaveFolder.Path;
                string token = StorageApplicationPermissions.FutureAccessList.Add(StaticData.SaveFolder);
                SettingManager.SetSaveMode(SaveLocationMode.SelectedFolder, SaveFolderPath, token);
                return true;
            }
            return false;
        }

        public void Initialize()
        {
            VersionDescription = GetVersionDescription();
        }

        private static SaveLocationMode GetSaveMode()
        {
            var saveMode = SettingManager.GetSaveMode();
            if (saveMode == SaveLocationMode.None || saveMode == SaveLocationMode.SameFolder)
            {
                //default
                saveMode = SaveLocationMode.Choose;
                SettingManager.SetSaveMode(SaveLocationMode.Choose);
            }

            return saveMode;
        }

        private static string GetVersionDescription()
        {
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{package.DisplayName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        private async void ChangeSaveMode(SaveLocationMode param)
        {
            SaveLocationMode = param;
            switch (SaveLocationMode)
            {
                case SaveLocationMode.SameFolder:
                    //Save in the same folder
                    SettingManager.SetSaveMode(SaveLocationMode.SameFolder);
                    break;
                case SaveLocationMode.Choose:
                    //Choose where to save
                    SettingManager.SetSaveMode(SaveLocationMode.Choose);
                    break;
                case SaveLocationMode.SelectedFolder:
                    //Save in specific folder
                    if (string.IsNullOrEmpty(SaveFolderPath))
                    {
                        bool isSuccess = await BrowseToSaveFolder();
                        if (!isSuccess)
                        {
                            SettingManager.SetSaveMode(SaveLocationMode.Choose);
                            SaveLocationMode = SaveLocationMode.Choose;
                        }
                    }

                    break;
            }
        }

        private async void ChangeTheme(ElementTheme param)
        {
            ElementTheme = param;
            await ThemeSelectorService.SetThemeAsync(param);
        }
    }
}
