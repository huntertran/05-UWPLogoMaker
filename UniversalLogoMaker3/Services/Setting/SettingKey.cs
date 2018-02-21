namespace UniversalLogoMaker3.Services.Setting
{
    public enum SettingKey
    {
        SaveMode,
        SavePath,
        SaveToken,
        DatabaseVersion
    }

    public enum SaveLocationMode
    {
        None = 0,
        SameFolder = 1,
        Choose = 2,
        SelectedFolder = 3
    }
}
