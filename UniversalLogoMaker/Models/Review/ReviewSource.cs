namespace UniversalLogoMaker.Models.Review
{
    using System;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml.Input;
    using Infrastructure;
    using Utilities.Helpers;

    public class ReviewSource : NotifyPropertyChangedImplementation
    {
        private PackedReviewSource _squareLogo;
        private PackedReviewSource _wideLogo;

        public PackedReviewSource SquareLogo
        {
            get => _squareLogo;
            set
            {
                if (Equals(value, _squareLogo)) return;
                _squareLogo = value;
                OnPropertyChanged();
            }
        }

        public PackedReviewSource WideLogo
        {
            get => _wideLogo;
            set
            {
                if (Equals(value, _wideLogo)) return;
                _wideLogo = value;
                OnPropertyChanged();
            }
        }

        public ReviewSource()
        {
            WideLogo = new PackedReviewSource();
            SquareLogo = new PackedReviewSource();
        }

        public async Task LoadImage(object sender, TappedRoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".bmp");

            var file = await openPicker.PickSingleFileAsync();

            if (file == null)
            {
                AppHelper.SetStatus("File not open");
                return;
            }

            using (IRandomAccessStream imageStream = await file.OpenAsync(FileAccessMode.Read))
            {
                var squareImageStream = imageStream.CloneStream();

                await WideLogo.SetSourceTask(imageStream);
                await SquareLogo.SetSourceTask(squareImageStream);
            }
        }
    }
}
