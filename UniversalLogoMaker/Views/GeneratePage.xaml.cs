namespace UniversalLogoMaker.Views
{
    using System;
    using System.Numerics;
    using System.Threading.Tasks;
    using Windows.Foundation;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI;
    using Windows.UI.Xaml.Input;
    using Microsoft.Graphics.Canvas;
    using Microsoft.Graphics.Canvas.Effects;
    using Microsoft.Graphics.Canvas.UI;
    using Microsoft.Graphics.Canvas.UI.Xaml;
    using ViewModels;

    public sealed partial class GeneratePage
    {
        public GenerateViewModel ViewModel { get; } = new GenerateViewModel();

        Random rnd = new Random();
        GaussianBlurEffect blur;
        private Vector2 RndPosition()
        {
            double x = rnd.NextDouble() * 500f;
            double y = rnd.NextDouble() * 500f;
            return new Vector2((float)x, (float)y);
        }

        private float RndRadius()
        {
            return (float)rnd.NextDouble() * 150f;
        }

        private byte RndByte()
        {
            return (byte)rnd.Next(256);
        }

        public GeneratePage()
        {
            InitializeComponent();
        }

        private void WideCanvasAnimatedControl_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            // Clear color
            args.DrawingSession.Clear(ViewModel.SelectedColor);

            if (_transperentBitmap != null)
            {
                //Draw transperent bitmap
                args.DrawingSession.DrawImage(_transperentBitmap, 0, 0, new Rect(0, 0, 620, 300), 1.0f);
            }

            //Fill the rectangle with color
            args.DrawingSession.FillRectangle(0, 0, 620, 300, ViewModel.SelectedColor);
        }

        private async void WideCanvasAnimatedControl_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            //CanvasCommandList cl = new CanvasCommandList(sender);
            //using (CanvasDrawingSession clds = cl.CreateDrawingSession())
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        clds.DrawText("Hello, World!", RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
            //        clds.DrawCircle(RndPosition(), RndRadius(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
            //        clds.DrawLine(RndPosition(), RndPosition(), Color.FromArgb(255, RndByte(), RndByte(), RndByte()));
            //    }
            //}

            //blur = new GaussianBlurEffect()
            //{
            //    Source = cl,
            //    BlurAmount = 10.0f
            //};

            await CreateResource(sender, args);
        }

        public async Task CreateResource(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            _device = sender.Device;

            await LoadFileResources();

            var cl = new CanvasCommandList(sender.Device);
            using (var clds = cl.CreateDrawingSession())
            {
                // Clear color
                clds.Clear(ViewModel.SelectedColor);

                //Draw transperent bitmap
                clds.DrawImage(_transperentBitmap, 0, 0, new Rect(0, 0, 620, 300), 1.0f);

                //Fill the rectangle with color
                clds.FillRectangle(0, 0, 620, 300, ViewModel.SelectedColor);
            }
        }

        public StorageFile File { get; set; }

        public CanvasBitmap UserBitmap { get; set; }

        private CanvasBitmap _transperentBitmap;

        private CanvasDevice _device;

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
    }
}
