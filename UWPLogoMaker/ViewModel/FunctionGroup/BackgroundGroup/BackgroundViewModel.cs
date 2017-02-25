using UWPLogoMaker.Model;

namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    public class BackgroundViewModel : PropertyChangedImplementation
    {
        private IBackgroundDrawable _backgroundDrawable;
        private BackgroundMode _backgroundMode;

        public MainViewModel MainVm;
        
        public IBackgroundDrawable ColorBackgroundVm
        {
            get { return _backgroundDrawable; }
            set
            {
                if (Equals(value, _backgroundDrawable)) return;
                _backgroundDrawable = value;
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