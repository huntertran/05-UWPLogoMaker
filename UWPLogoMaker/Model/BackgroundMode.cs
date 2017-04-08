namespace UWPLogoMaker.Model
{
    using System;
    using ViewModel;

    public enum BackgroundMode
    {
        SolidColorBrush,
        GradientColorBrush,
        SamplePattern,
        Geometry
    }

    public class AvailableBackgroundMode : PropertyChangedImplementation
    {
        private BackgroundMode _backgroundMode;
        private Type _classToNavigate;

        public BackgroundMode BackgroundMode
        {
            get => _backgroundMode;
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
            get => _classToNavigate;
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
