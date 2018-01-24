namespace UniversalLogoMaker.Views
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Graphics.Canvas;
    using Microsoft.Graphics.Canvas.UI;
    using Microsoft.Graphics.Canvas.UI.Xaml;
    using ViewModels;
    using Windows.Foundation;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Input;

    public sealed partial class GeneratePage
    {
        private CanvasDevice _device;
        private CanvasBitmap _transperentBitmap;

        public GeneratePage()
        {
            InitializeComponent();
        }

        public StorageFile File { get; set; }

        public CanvasBitmap UserBitmap { get; set; }

        public GenerateViewModel ViewModel { get; } = new GenerateViewModel();

        public async Task CreateResource(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            _device = sender.Device;

            await LoadFileResources();

            var cl = new CanvasCommandList(sender.Device);
            using (var clds = cl.CreateDrawingSession())
            {
                DrawPreview(clds);
            }
        }

        private void DrawPreview(CanvasDrawingSession clds)
        {
            // Clear color
            clds.Clear(ViewModel.SelectedColor);

            if (_transperentBitmap != null)
            {
                //Draw transperent bitmap
                clds.DrawImage(_transperentBitmap, 0, 0, new Rect(0, 0, 620, 300), 1.0f);
            }

            //Fill the rectangle with color
            clds.FillRectangle(0, 0, 620, 300, ViewModel.SelectedColor);
        }

        private async void ChooseImageButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".bmp");

            File = await openPicker.PickSingleFileAsync();

            if (File == null)
            {
                return;
            }

            UnRegisterCanvasAnimatedControlEvents();
            RegisterCanvasAnimatedControlEvents();
        }

        private async Task LoadFileResources()
        {
            using (var fileStream = await File.OpenAsync(FileAccessMode.Read))
            {
                //User Bitmap
                UserBitmap = await CanvasBitmap.LoadAsync(_device, fileStream);
            }

            var file = await StorageFile.GetFileFromApplicationUriAsync(
                new Uri(@"ms-appx:///Assets/Resources/checkerboard.png"));

            using (var fileStream = await file.OpenAsync(FileAccessMode.Read))
            {
                //Transperent Bitmap
                _transperentBitmap = await CanvasBitmap.LoadAsync(_device, fileStream);
            }
        }

        private void RegisterCanvasAnimatedControlEvents()
        {
            WideCanvasAnimatedControl.CreateResources += WideCanvasAnimatedControl_CreateResources;
            WideCanvasAnimatedControl.Draw += WideCanvasAnimatedControl_Draw;
        }

        private void UnRegisterCanvasAnimatedControlEvents()
        {
            WideCanvasAnimatedControl.CreateResources -= WideCanvasAnimatedControl_CreateResources;
            WideCanvasAnimatedControl.Draw -= WideCanvasAnimatedControl_Draw;
        }

        private async void WideCanvasAnimatedControl_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            await CreateResource(sender, args);
        }

        private void WideCanvasAnimatedControl_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            DrawPreview(args.DrawingSession);
        }
    }
}
