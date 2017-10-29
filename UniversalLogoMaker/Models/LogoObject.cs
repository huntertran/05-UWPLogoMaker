namespace UniversalLogoMaker.Models
{
    using Infrastructure;
    using System;

    public class LogoObject : NotifyPropertyChangedImplementation
    {
        private string _fileName;
        private int _width;
        private int _height;
        private int _scale;
        private int _defaultScale = 100;
        private const string DefaultRatio = "310:150";

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
        
        public LogoObject()
        {
        }

        public LogoObject(string name, int widthSize, int height)
        {
            FileName = name;
            Scale = _defaultScale;
            Width = widthSize;
            Height = height;
        }

        public LogoObject(string name, int scale, int widthSize, bool isSquare, string ratio = DefaultRatio)
        {
            FileName = name;
            Scale = scale;
            Width = (int)Math.Ceiling((double)(widthSize * scale) / _defaultScale);
            if (isSquare)
            {
                Height = (int)Math.Ceiling((double)(widthSize * scale) / _defaultScale);
            }
            else
            {
                int upLeft = Convert.ToInt32(ratio.Split(':')[0]);
                int downLeft = Convert.ToInt32(ratio.Split(':')[1]);

                Height = (int)Math.Ceiling((double)(widthSize * downLeft / upLeft * scale) / _defaultScale);
            }
        }
    }
}