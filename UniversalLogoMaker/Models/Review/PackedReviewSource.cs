namespace UniversalLogoMaker.Models.Review
{
    using Infrastructure;
    using System;
    using System.Threading.Tasks;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml.Media.Imaging;

    public class PackedReviewSource : NotifyPropertyChangedImplementation
    {
        private BitmapImage _reviewBitmapImage;
        private double _xPosition;
        private double _yPosition;
        private double _zPosition;

        public BitmapImage ReviewBitmapImage
        {
            get => _reviewBitmapImage;
            set
            {
                if (Equals(value, _reviewBitmapImage)) return;
                _reviewBitmapImage = value;
                OnPropertyChanged();
            }
        }

        public double XPosition
        {
            get => _xPosition;
            set
            {
                if (value.Equals(_xPosition)) return;
                _xPosition = value;
                OnPropertyChanged();
            }
        }

        public double YPosition
        {
            get => _yPosition;
            set
            {
                if (value.Equals(_yPosition)) return;
                _yPosition = value;
                OnPropertyChanged();
            }
        }

        public double ZPosition
        {
            get => _zPosition;
            set
            {
                if (value.Equals(_zPosition)) return;
                _zPosition = value;
                OnPropertyChanged();
            }
        }

        public PackedReviewSource()
        {
            ReviewBitmapImage = new BitmapImage();
        }

        public async Task SetSourceTask(IRandomAccessStream fileStream)
        {
            await ReviewBitmapImage.SetSourceAsync(fileStream);
        }
    }
}
