namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    using System.Collections.ObjectModel;
    using Model;
    using View.FunctionGroup.BackgroundGroup;

    public class BackgroundViewModel : PropertyChangedImplementation
    {
        private IBackgroundDrawable _backgroundDrawable;

        public MainViewModel MainVm;
        private ObservableCollection<AvailableBackgroundMode> _availableBackgroundModes;

        public IBackgroundDrawable ColorBackgroundVm
        {
            get => _backgroundDrawable;
            set
            {
                if (Equals(value, _backgroundDrawable)) return;
                _backgroundDrawable = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AvailableBackgroundMode> AvailableBackgroundModes
        {
            get => _availableBackgroundModes;
            set
            {
                if (Equals(value, _availableBackgroundModes))
                    return;
                _availableBackgroundModes = value;
                OnPropertyChanged();
            }
        }

        public BackgroundViewModel(MainViewModel mainVm)
        {
            MainVm = mainVm;
            Initialize();
            ColorBackgroundVm = new ColorBackgroundViewModel(this);
        }

        private void Initialize()
        {
            AvailableBackgroundModes = new ObservableCollection<AvailableBackgroundMode>();

            AvailableBackgroundMode availableBackgroundMode = new AvailableBackgroundMode
            {
                BackgroundMode = BackgroundMode.SolidColorBrush,
                ClassToNavigate = typeof(ColorPage)
            };

            AvailableBackgroundModes.Add(availableBackgroundMode);
        }
    }
}