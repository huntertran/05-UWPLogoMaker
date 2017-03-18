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
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Icon
        {
            get { return _icon; }
            set
            {
                if (value == _icon) return;
                _icon = value;
                OnPropertyChanged();
            }
        }

        public Type PageType
        {
            get { return _pageType; }
            set
            {
                if (value == _pageType) return;
                _pageType = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value.Equals(_isEnabled)) return;
                _isEnabled = value;
                OnPropertyChanged();
            }
        }
    }
}
