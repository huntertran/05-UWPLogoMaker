namespace UWPLogoMaker.ViewModel
{
    using Windows.ApplicationModel.Resources;
    using Windows.Storage;
    using NewSizeGroup;
    using StartGroup;

    public class StaticData : PropertyChangedImplementation
    {
        public static StorageFolder SaveFolder;
        public static StartViewModel StartVm = new StartViewModel();
        public static NewSizeViewModel NewSizeVm = new NewSizeViewModel();

        public static ResourceLoader LanguageResources = ResourceLoader.GetForCurrentView();
    }
}
