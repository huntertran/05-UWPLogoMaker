namespace UniversalLogoMaker3.Models
{
    using System.Collections.Generic;
    using Helpers;

    public class Platform : Observable
    {
        private string _name;
        private string _icon;
        private List<LogoObject> _saveLogoList;
        private bool _isEnabled;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Icon
        {
            get => _icon;
            set => Set(ref _icon, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => Set(ref _isEnabled, value);
        }

        public List<LogoObject> SaveLogoList
        {
            get => _saveLogoList;
            set => Set(ref _saveLogoList, value);
        }
    }
}