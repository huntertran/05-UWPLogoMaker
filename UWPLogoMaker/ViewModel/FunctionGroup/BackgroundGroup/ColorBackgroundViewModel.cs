namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    using System.Globalization;
    using Windows.UI;
    using Windows.UI.Xaml.Media;

    public class ColorBackgroundViewModel : BackgroundDrawable
    {
        private Brush _currentBrush;
        private Color _currentColor;

        private double _r = 255;
        private double _g = 255;
        private double _b = 255;
        private double _a = 255;
        private string _hexaCode;

        public Brush CurrentBrush
        {
            get { return _currentBrush; }
            set
            {
                if (Equals(value, _currentBrush)) return;
                _currentBrush = value;
                //ChangeBackgroundColor();
                OnPropertyChanged();
            }
        }

        public override Color CurrentColor
        {
            get { return _currentColor; }
            set
            {
                if (Equals(value, _currentColor)) return;
                _currentColor = value;
                OnPropertyChanged();
            }
        }

        public double R
        {
            get { return _r; }
            set
            {
                if (value == _r) return;
                _r = value;

                OnPropertyChanged();
                ChangeColor();
            }
        }

        public double G
        {
            get { return _g; }
            set
            {
                if (value == _g) return;
                _g = value;

                OnPropertyChanged();
                ChangeColor();
            }
        }

        public double B
        {
            get { return _b; }
            set
            {
                if (value == _b) return;
                _b = value;

                OnPropertyChanged();
                ChangeColor();
            }
        }

        public double A
        {
            get { return _a; }
            set
            {
                if (value == _a) return;
                _a = value;

                OnPropertyChanged();
                ChangeColor();
            }
        }

        public string HexaCode
        {
            get { return _hexaCode; }
            set
            {
                if (value == _hexaCode) return;
                _hexaCode = value;

                OnPropertyChanged();
                ChangeColorFromHexa();
            }
        }

        public ColorBackgroundViewModel(BackgroundViewModel backgroundVm) : base(backgroundVm)
        {
        }

        public new void ChangeColor()
        {
            CurrentBrush = new SolidColorBrush(Color.FromArgb((byte)A, (byte)R, (byte)G, (byte)B));
            HexaCode = "#" + ((byte)A).ToString("X2") + ((byte)R).ToString("X2") + ((byte)G).ToString("X2") +
                       ((byte)B).ToString("X2");
        }

        public void ChangeColorFromHexa()
        {
            //Remove # if present
            if (HexaCode.IndexOf('#') != -1)
                HexaCode = HexaCode.Replace("#", "");

            if (HexaCode.Length == 8)
            {
                //#AARRGGBB
                A = int.Parse(HexaCode.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                R = int.Parse(HexaCode.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                G = int.Parse(HexaCode.Substring(4, 2), NumberStyles.AllowHexSpecifier);
                B = int.Parse(HexaCode.Substring(6, 2), NumberStyles.AllowHexSpecifier);
            }
            if (HexaCode.Length == 6)
            {
                //#RRGGBB
                R = int.Parse(HexaCode.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                G = int.Parse(HexaCode.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                B = int.Parse(HexaCode.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            }
            else if (HexaCode.Length == 3)
            {
                //#RGB
                R = int.Parse(HexaCode[0] + HexaCode[0].ToString(), NumberStyles.AllowHexSpecifier);
                G = int.Parse(HexaCode[1] + HexaCode[1].ToString(), NumberStyles.AllowHexSpecifier);
                B = int.Parse(HexaCode[2] + HexaCode[2].ToString(), NumberStyles.AllowHexSpecifier);
            }
        }

        public new void Update()
        {
            BackgroundVm.MainVm.DisplayPreview();

            BackgroundVm.MainVm.DisplaySquarePreview();

            BackgroundVm.MainVm.InvalidateCanvasControl();
        }
    }
}
