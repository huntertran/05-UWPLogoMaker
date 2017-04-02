namespace UWPLogoMaker.Model
{
    using System.Collections.ObjectModel;
    using ViewModel;

    public class Database : PropertyChangedImplementation
    {
        private ObservableCollection<Platform> _platformList;
        private int _databaseVersion;
        private string _updateMessage;
        private int _isShow;

        public ObservableCollection<Platform> PlatformList
        {
            get { return _platformList; }
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
            get { return _databaseVersion; }
            set
            {
                if (value == _databaseVersion) return;
                _databaseVersion = value;
                OnPropertyChanged();
            }
        }

        public string UpdateMessage
        {
            get { return _updateMessage; }
            set
            {
                if (value == _updateMessage) return;
                _updateMessage = value;
                OnPropertyChanged();
            }
        }

        public int IsShow
        {
            get { return _isShow; }
            set
            {
                if (value == _isShow) return;
                _isShow = value;
                OnPropertyChanged();
            }
        }
    }
}
