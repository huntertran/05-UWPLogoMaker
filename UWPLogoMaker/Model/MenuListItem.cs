namespace UWPLogoMaker.Model
{
    using System;
    using ViewModel;

    public class MenuListItem : PropertyChangedImplementation
    {
        private string _name;
        private string _icon;
        private Type _pageType;
        private bool _isEnabled;

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Icon
        {
            get => _icon;
            set
            {
                if (value == _icon) return;
                _icon = value;
                OnPropertyChanged();
            }
        }

        public Type PageType
        {
            get => _pageType;
            set
            {
                if (value == _pageType) return;
                _pageType = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (value.Equals(_isEnabled)) return;
                _isEnabled = value;
                OnPropertyChanged();
            }
        }
    }
}
