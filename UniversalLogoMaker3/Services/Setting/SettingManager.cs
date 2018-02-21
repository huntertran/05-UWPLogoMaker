namespace UniversalLogoMaker3.Services.Setting
{
    public class SettingManager
    {
        public static SaveLocationMode GetSaveMode()
        {
            var saveMode = SettingService.GetSetting<int>(SettingKey.SaveMode.ToString());
            return (SaveLocationMode) saveMode;
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
        public static void SetSaveMode(SaveLocationMode saveMode, string path = null, string token = null)
        {
            SettingService.SetSetting(SettingKey.SaveMode.ToString(), (int) saveMode);
            if (path != null)
            {
                SettingService.SetSetting(SettingKey.SavePath.ToString(), path);
                SettingService.SetSetting(SettingKey.SaveToken.ToString(), token);
            }
        }

        public static string GetSavePath()
        {
            return SettingService.GetSetting<string>(SettingKey.SavePath.ToString());
        }

        public static string GetSaveToken()
        {
            return SettingService.GetSetting<string>(SettingKey.SaveToken.ToString());
        }
    }
}
