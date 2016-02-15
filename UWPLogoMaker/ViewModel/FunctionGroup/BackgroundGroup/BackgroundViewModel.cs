using UWPLogoMaker.Model;

namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    public class BackgroundViewModel : BaseViewModel
    {
        private ColorBackgroundViewModel _colorBackgroundVm;

        public MainViewModel MainVm;

        private BackgroundMode _backgroundMode;

        public ColorBackgroundViewModel ColorBackgroundVm
        {
            get { return _colorBackgroundVm; }
            set
            {
                if (Equals(value, _colorBackgroundVm)) return;
                _colorBackgroundVm = value;
                OnPropertyChanged();
            }
        }

        public BackgroundMode BackgroundMode
        {
            get { return _backgroundMode; }
            set
            {
                if (value == _backgroundMode) return;
                _backgroundMode = value;
                OnPropertyChanged();
            }
        }

        public BackgroundViewModel(MainViewModel mainVm)
        {
            MainVm = mainVm;
            ColorBackgroundVm = new ColorBackgroundViewModel(this);
        }
    }
}