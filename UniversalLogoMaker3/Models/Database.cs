namespace UniversalLogoMaker3.Models
{
    using System.Collections.ObjectModel;
    using Helpers;

    public class Database : Observable
    {
        private ObservableCollection<Platform> _platformList;
        private int _databaseVersion;
        private string _updateMessage;
        private int _isShow;

        public ObservableCollection<Platform> PlatformList
        {
            get => _platformList;
            set => Set(ref _platformList, value);
        }

        public int DatabaseVersion
        {
            get => _databaseVersion;
            set => Set(ref _databaseVersion, value);
        }

        public string UpdateMessage
        {
            get => _updateMessage;
            set => Set(ref _updateMessage, value);
        }

        public int IsShow
        {
            get => _isShow;
            set => Set(ref _isShow, value);
        }
    }
}