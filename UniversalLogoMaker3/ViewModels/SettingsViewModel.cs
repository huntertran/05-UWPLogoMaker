namespace UniversalLogoMaker3.ViewModels
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Helpers;
    using Infrastructure;
    using Models;
    using Newtonsoft.Json.Linq;
    using Services;
    using Services.Setting;
    using Windows.ApplicationModel;
    using Windows.Storage.AccessCache;
    using Windows.Storage.Pickers;
    using Windows.System;
    using Windows.UI.Xaml;

    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/pages/settings.md
    public class SettingsViewModel : Observable
    {
        private ICommand _checkDatabaseUpdateCommand;
        private Database _data;
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;
        private ICommand _feedbackCommand;
        private ICommand _rateCommand;
        private string _saveFolderPath;
        private ICommand _saveLocationCommand;
        private SaveLocationMode _saveLocationMode = GetSaveMode();
        private ICommand _switchThemeCommand;
        private string _versionDescription;

        public ICommand CheckDatabaseUpdateCommand
        {
            get
            {
                if (_checkDatabaseUpdateCommand != null)
                {
                    return _checkDatabaseUpdateCommand;
                }

                return _checkDatabaseUpdateCommand = new RelayCommand(CheckForDatabase);
            }
        }

        public Database Data
        {
            get => _data;
            set => Set(ref _data, value);
        }

        public ElementTheme ElementTheme
        {
            get => _elementTheme;
            set => Set(ref _elementTheme, value);
        }

        public ICommand FeedbackCommand
        {
            get
            {
                if (_feedbackCommand != null)
                {
                    return _feedbackCommand;
                }

                return _feedbackCommand = new RelayCommand(Feedback);
            }
        }

        public ICommand RateCommand
        {
            get
            {
                if (_rateCommand != null)
                {
                    return _rateCommand;
                }

                return _rateCommand = new RelayCommand(Rate);
            }
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

        public void Initialize()
        {
            VersionDescription = GetVersionDescription();
            GetLocalDatabase();
        }

        private static async void Feedback()
        {
            await Launcher.LaunchUriAsync(
                    new Uri("mailto:hunter.tran.92@outlook.com?subject=[Universal Logo Maker for Windows] Feedback&body=Hello, "));
        }

        private static async Task<Database> GetDatabaseFromApi()
        {
            var connection = new Connection();
            if (!connection.HasInternetAccess)
            {
                return null;
            }

            var result =
                await HttpService.GetHttpAsString("https://sites.google.com/site/windowsstoreapplogomaker/");

            var json = Regex.Split(result, "~~~")[1];

            var jObject = JObject.Parse(json);
            return jObject.ToObject<Database>();
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

        private static async void Rate()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9nblggh5zchk"));
        }

        private async Task<bool> BrowseToSaveFolder()
        {
            FolderPicker fPicker = new FolderPicker { SuggestedStartLocation = PickerLocationId.PicturesLibrary };
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

        private async void CheckForDatabase()
        {
            await UpdateDatabase();
        }

        private async void GetLocalDatabase()
        {
            Data = await StorageService.Json2Object<Database>("data.dat");
        }

        private async Task UpdateDatabase()
        {
            var tempDb = await GetDatabaseFromApi();

            if (tempDb?.DatabaseVersion > Data.DatabaseVersion)
            {
                Data = tempDb;

                //Save to roaming folder
                await StorageService.Object2Json(Data, "data.dat");
            }
        }
    }
}
