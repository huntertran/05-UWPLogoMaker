namespace UniversalLogoMaker3.Models
{
    using System;
    using Helpers;

    public class LogoObject : Observable
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
                    OnPropertyChanged(nameof(Width));
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
                    OnPropertyChanged(nameof(Height));
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
                    OnPropertyChanged(nameof(Scale));
                }
            }
        }

        public string FileName
        {
            get => _fileName;
            set => Set(ref _fileName, value);
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
            Width = (int)Math.Ceiling((double)(widthSize * scale) / 100);
            if (isSquare)
            {
                Height = (int)Math.Ceiling((double)(widthSize * scale) / 100);
            }
            else
            {
                int upLeft = Convert.ToInt32(ratio.Split(':')[0]);
                int downLeft = Convert.ToInt32(ratio.Split(':')[1]);

                Height = (int)Math.Ceiling((double)(widthSize * downLeft / upLeft * scale) / 100);
            }
        }
    }
}
