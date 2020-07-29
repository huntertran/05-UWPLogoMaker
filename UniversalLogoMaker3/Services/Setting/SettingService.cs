namespace UniversalLogoMaker3.Services.Setting
{
    using System;
    using Windows.Storage;

    public class SettingService
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
}
