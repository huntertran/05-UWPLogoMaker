namespace UniversalLogoMaker.Models
{
    using Infrastructure;
    using System.Collections.Generic;

    public class Platform : NotifyPropertyChangedImplementation
    {
        private string _name;
        private string _icon;
        private List<LogoObject> _saveLogoList;
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

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (value == _isEnabled) return;
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public List<LogoObject> SaveLogoList
        {
            get => _saveLogoList;
            set
            {
                if (Equals(value, _saveLogoList)) return;
                _saveLogoList = value;
                OnPropertyChanged();
            }
        }
    }
}
