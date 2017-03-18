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
            get { return _saveFolderPath; }
            set
            {
                if (value == _saveFolderPath) return;
                _saveFolderPath = value;
                OnPropertyChanged();
            }
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

            if (StaticData.SaveFolder == null) return false;

            SaveFolderPath = StaticData.SaveFolder.Path;
            string token =
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(StaticData.SaveFolder);
            SettingManager.SetSaveMode(SaveMode.SpecificFoler, SaveFolderPath, token);
            return true;
        }
    }
}