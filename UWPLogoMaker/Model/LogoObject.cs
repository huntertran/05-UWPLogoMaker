namespace UWPLogoMaker.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using ViewModel;

    public class LogoObject : PropertyChangedImplementation
    {
        private string _fileName;
        private int _width, _height, _scale;

        public int Width
        {
            get => _width;
            set
            {
                if (value > 0)
                {
                    if (value == _width) return;
                    _width = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                if (value > 0)
                {
                    if (value == _height) return;
                    _height = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Scale
        {
            get => _scale;
            set
            {
                if (value > 0)
                {
                    if (value == _scale) return;
                    _scale = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FileName
        {
            get => _fileName;
            set
            {
                if (value == _fileName) return;
                _fileName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Default Constructor, required for Json serialization
        /// </summary>
        public LogoObject()
        {
        }

        public LogoObject(string name, int widthSize, int height)
        {
            FileName = name;
            Scale = 100;
            Width = widthSize;
            Height = height;
        }

        public LogoObject(string name, int scale, int widthSize, bool isSquare, string ratio = "310:150")
        {
            FileName = name;
            Scale = scale;
            Width = (int) Math.Ceiling((double) (widthSize * scale) / 100);
            if (isSquare)
            {
                Height = (int) Math.Ceiling((double) (widthSize * scale) / 100);
            }
            else
            {
                var upLeft = Convert.ToInt32(ratio.Split(':')[0]);
                var downLeft = Convert.ToInt32(ratio.Split(':')[1]);

                Height = (int) Math.Ceiling((double) (widthSize * downLeft / upLeft * scale) / 100);
            }
        }
    }

    public class Platform : PropertyChangedImplementation
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

    public class Database : PropertyChangedImplementation
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