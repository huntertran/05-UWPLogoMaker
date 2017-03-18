namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    using System;
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
            get { return _backgroundDrawable; }
            set
            {
                if (Equals(value, _backgroundDrawable)) return;
                _backgroundDrawable = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AvailableBackgroundMode> AvailableBackgroundModes
        {
            get { return _availableBackgroundModes; }
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
            AvailableBackgroundMode availableBackgroundMode = new AvailableBackgroundMode();
            availableBackgroundMode.BackgroundMode = BackgroundMode.SolidColorBrush;
            availableBackgroundMode.ClassToNavigate = typeof(ColorPage);
            AvailableBackgroundModes.Add(availableBackgroundMode);
        }
    }

    public class AvailableBackgroundMode : PropertyChangedImplementation
    {
        private BackgroundMode _backgroundMode;
        private Type _classToNavigate;

        public BackgroundMode BackgroundMode
        {
            get { return _backgroundMode; }
            set
            {
                if (Equals(value, _backgroundMode))
                    return;
                _backgroundMode = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(BackgroundModeName));
            }
        }

        public Type ClassToNavigate
        {
            get { return _classToNavigate; }
            set
            {
                if (value == _classToNavigate)
                    return;
                _classToNavigate = value;
                OnPropertyChanged();
            }
        }

        public string BackgroundModeName
        {
            get
            {
                switch (BackgroundMode)
                {
                    case BackgroundMode.GradientColorBrush:
                        return "Gradient Color";
                    case BackgroundMode.SamplePattern:
                        return "Sample Pattern";
                    case BackgroundMode.SolidColorBrush:
                        return "Solid Color";
                    default:
                        return BackgroundMode.ToString();
                }
            }
        }
    }
}