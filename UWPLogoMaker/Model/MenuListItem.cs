﻿using UWPLogoMaker.ViewModel;

namespace UWPLogoMaker.Model
{
    public enum MenuFunc
    {
        Uwp,
        Wp7,
        Wp8,
        W8,
        Custom,
        Add,
        Settings,
        About
    };

    public class MenuListItem : BaseViewModel
    {

        private string _name;
        private string _icon;
        private MenuFunc _menuF;
        private bool _isEnabled = false;

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

        public MenuFunc MenuF
        {
            get { return _menuF; }
            set
            {
                if (value == _menuF) return;
                _menuF = value;
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
