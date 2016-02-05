using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UWPLogoMaker.ViewModel;

namespace UWPLogoMaker.Model
{
    public class LogoObject : BaseViewModel
    {
        private string _fileName;
        private int _width, _height, _scale;

        public int Width
        {
            get { return _width; }
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
            get { return _height; }
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
            get { return _scale; }
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
            get { return _fileName; }
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

        //public LogoObject(string file, int scale, int width = -1, int height = -1)
        //{
        //    FileName = file;
        //    Scale = scale;
        //    Width = (width == -1) ? Scale : width;
        //    Height = (height == -1) ? Width : height;
        //}

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
            Width = (int)Math.Ceiling((double)(widthSize * scale) / 100);
            if (isSquare)
            {
                Height = (int)Math.Ceiling((double)(widthSize * scale) / 100);
            }
            else
            {
                int upLeft = Convert.ToInt32(ratio.Split(':')[0]);
                int downLeft = Convert.ToInt32(ratio.Split(':')[1]);

                Height = (int)Math.Ceiling((double)(((widthSize * downLeft) / upLeft) * scale) / 100);
            }
        }

        
    }

    public class Platform : BaseViewModel
    {
        private string _name;
        private string _icon;
        private List<LogoObject> _saveLogoList;
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

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value == _isEnabled) return;
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public List<LogoObject> SaveLogoList
        {
            get { return _saveLogoList; }
            set
            {
                if (Equals(value, _saveLogoList)) return;
                _saveLogoList = value;
                OnPropertyChanged();
            }
        }
    }

    public class Database : BaseViewModel
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