namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    public class BackgroundViewModel : BaseViewModel
    {
        private ColorBackgroundViewModel _colorBackgroundVm;

        public MainViewModel MainVm;

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

        public BackgroundViewModel(MainViewModel mainVm)
        {
            MainVm = mainVm;
            ColorBackgroundVm = new ColorBackgroundViewModel(this);
        }
    }
}