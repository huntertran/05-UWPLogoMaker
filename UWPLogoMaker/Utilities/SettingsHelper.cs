namespace UWPLogoMaker.Utilities
{
    using System;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Windows.Storage.AccessCache;
    using Windows.UI.Popups;
    using ViewModel;

    public class SettingsHelper
    {
        public static void SetSetting<T>(string key, T value)
        {
            ApplicationData.Current.LocalSettings.Values[key] = value;
        }

        public static T GetSetting<T>(string key)
        {
            try
            {
                return (T) ApplicationData.Current.LocalSettings.Values[key];
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }

    public sealed class SettingKey
    {
        public static readonly SettingKey SaveMode = new SettingKey("SaveMode");
        public static readonly SettingKey SavePath = new SettingKey("SavePath");
        public static readonly SettingKey SaveToken = new SettingKey("SaveToken");
        public static readonly SettingKey DatabaseVersion = new SettingKey("DatabaseVersion");

        private SettingKey(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }

    public enum SaveMode
    {
        None,
        SameFolder,
        UserChooseToSave,
        SpecificFoler
    }

    public class SettingManager
    {
        public static async Task<SaveMode> GetSaveMode(bool checkFolder = false)
        {
            int saveModeInt = SettingsHelper.GetSetting<int>(SettingKey.SaveMode.Value);
            SaveMode saveMode = (SaveMode) saveModeInt;

            if (saveMode == SaveMode.None || saveMode == SaveMode.SameFolder)
            {
                //Default
                saveMode = SaveMode.UserChooseToSave;
                SetSaveMode(saveMode);
            }

            if (saveMode == SaveMode.SpecificFoler)
            {
                //Check if folder is deleted
                try
                {
                    string token = GetSaveToken();
                    StaticData.SaveFolder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
                }
                catch
                {
                    //Set default save mode
                    saveMode = SaveMode.UserChooseToSave;
                    SetSaveMode(saveMode);

                    MessageDialog msg = new MessageDialog("Your selected folder has been deleted or moved", "Warning");
                    await msg.ShowAsync();
                }
            }

            return saveMode;
        }

        /// <summary>
        /// Set save mode
        /// 1: Save in the same folder
        /// 2: Choose where to save
        /// 3: Save in specific folder
        /// </summary>
        /// <param name="saveMode">save mode</param>
        /// <param name="path">path to specific folder. Apply for 3</param>
        /// <param name="token">token to access later. Apply for 3</param>
        public static void SetSaveMode(SaveMode saveMode, string path = null, string token = null)
        {
            SettingsHelper.SetSetting(SettingKey.SaveMode.Value, (int)saveMode);
            if (path != null)
            {
                SettingsHelper.SetSetting(SettingKey.SavePath.Value, path);
                SettingsHelper.SetSetting(SettingKey.SaveToken.Value, token);
            }
        }

        public static string GetSavePath()
        {
            return SettingsHelper.GetSetting<string>(SettingKey.SavePath.Value);
        }

        public static string GetSaveToken()
        {
            return SettingsHelper.GetSetting<string>(SettingKey.SaveToken.Value);
        }
    }
}
