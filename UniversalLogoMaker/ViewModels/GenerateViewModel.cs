namespace UniversalLogoMaker.ViewModels
{
    using Helpers;
    using Windows.UI;
    using Windows.UI.Xaml.Media;

    public class GenerateViewModel : Observable
    {
        private Color _selectedColor = Colors.AliceBlue;
        private float _x;
        private float _y;
        private double _rectX;
        private double _rectY;
        private double _rectWidth;
        private double _rectHeight;
        private double _zoomFactor;

        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                Set(ref _selectedColor, value);
                OnPropertyChanged(nameof(SelectedBrush));
            }
        }

        public Brush SelectedBrush => new SolidColorBrush(_selectedColor);

        public float X
        {
            get => _x;
            set => Set(ref _x, value);
        }

        public float Y
        {
            get => _y;
            set => Set(ref _y, value);
        }

        public double RectX
        {
            get => _rectX;
            set => Set(ref _rectX, value);
        }

        public double RectY
        {
            get => _rectY;
            set => Set(ref _rectY, value);
        }

        public double RectWidth
        {
            get => _rectWidth;
            set => Set(ref _rectWidth, value);
        }

        public double RectHeight
        {
            get => _rectHeight;
            set => Set(ref _rectHeight, value);
        }

        public double ZoomFactor
        {
            get => _zoomFactor;
            set => Set(ref _zoomFactor, value);
        }
    }
}
