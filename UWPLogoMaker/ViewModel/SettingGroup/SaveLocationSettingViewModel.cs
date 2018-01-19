namespace UWPLogoMaker.ViewModel.SettingGroup
{
    using System;
    using System.Threading.Tasks;
    using Windows.Storage.Pickers;
    using Utilities;

    public class SaveLocationSettingViewModel : PropertyChangedImplementation
    {
        private string _saveFolderPath;

        public string SaveFolderPath
        {
            get => _saveFolderPath;
            set
            {
                if (value == _saveFolderPath) return;
                _saveFolderPath = value;
                OnPropertyChanged();
            }
        }

        public async Task<bool> BrowseToSaveFolder()
        {
            var fPicker = new FolderPicker {SuggestedStartLocation = PickerLocationId.PicturesLibrary};
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
                var token =
                    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(
                        StaticData.SaveFolder);
                SettingManager.SetSaveMode(3, SaveFolderPath, token);
                return true;
            }

            return false;
        }
    }
}