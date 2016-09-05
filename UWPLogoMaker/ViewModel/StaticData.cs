using Windows.Storage;
using UWPLogoMaker.ViewModel.NewSizeGroup;
using UWPLogoMaker.ViewModel.StartGroup;

namespace UWPLogoMaker.ViewModel
{
    public class StaticData : BaseViewModel
    {
        public static StorageFolder SaveFolder;
        public static StartViewModel StartVm = new StartViewModel();
        public static NewSizeViewModel NewSizeVm = new NewSizeViewModel();
    }
}
