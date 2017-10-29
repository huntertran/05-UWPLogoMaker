﻿namespace UniversalLogoMaker.Models
{
    using Infrastructure;
    using System.Collections.ObjectModel;

    public class Database : NotifyPropertyChangedImplementation
    {
        private ObservableCollection<Platform> _platformList;
        private int _databaseVersion;
        private string _updateMessage;
        private int _isShow;

        public ObservableCollection<Platform> PlatformList
        {
            get => _platformList;
            set
            {
                if (_platformList == value)
                    return;
                _platformList = value;
                OnPropertyChanged();
            }
        }

        public int DatabaseVersion
        {
            get => _databaseVersion;
            set
            {
                if (value == _databaseVersion) return;
                _databaseVersion = value;
                OnPropertyChanged();
            }
        }

        public string UpdateMessage
        {
            get => _updateMessage;
            set
            {
                if (value == _updateMessage) return;
                _updateMessage = value;
                OnPropertyChanged();
            }
        }

        public int IsShow
        {
            get => _isShow;
            set
            {
                if (value == _isShow) return;
                _isShow = value;
                OnPropertyChanged();
            }
        }
    }
}
