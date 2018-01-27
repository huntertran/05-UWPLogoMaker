namespace UniversalLogoMaker.Views
{
    using System;
    using System.Numerics;
    using System.Threading.Tasks;
    using Microsoft.Graphics.Canvas;
    using Microsoft.Graphics.Canvas.Effects;
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
        private CanvasBitmap _userBitmap;

        public StorageFile File { get; set; }

        public GenerateViewModel ViewModel { get; } = new GenerateViewModel();

        public GeneratePage()
        {
            InitializeComponent();
            Unloaded += GeneratePage_Unloaded;
        }

        private void GeneratePage_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Prevent memory leaks
            UnRegisterCanvasAnimatedControlEvents();
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
                _userBitmap = await CanvasBitmap.LoadAsync(_device, fileStream);
            }

            var file = await StorageFile.GetFileFromApplicationUriAsync(
                new Uri(@"ms-appx:///Assets/Resources/checkerboard.png"));

            using (var fileStream = await file.OpenAsync(FileAccessMode.Read))
            {
                //Transperent Bitmap
                _transperentBitmap = await CanvasBitmap.LoadAsync(_device, fileStream);
            }
        }

        private async void WideCanvasAnimatedControl_CreateResources(CanvasAnimatedControl sender,
            CanvasCreateResourcesEventArgs args)
        {
            await CreateResource(sender, args);
        }

        public async Task CreateResource(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            _device = sender.Device;

            await LoadFileResources();

            Calculation();

            var cl = new CanvasCommandList(sender.Device);
            using (var clds = cl.CreateDrawingSession())
            {
                DrawPreview(clds);
            }
        }

        private void WideCanvasAnimatedControl_Draw(
            ICanvasAnimatedControl sender,
            CanvasAnimatedDrawEventArgs args)
        {
            DrawPreview(args.DrawingSession);
        }

        private void DrawPreview(CanvasDrawingSession clds)
        {
            if (clds == null)
            {
                return;
            }

            // Clear color
            clds.Clear(ViewModel.SelectedColor);

            if (_transperentBitmap != null)
            {
                //Draw transperent bitmap
                clds.DrawImage(_transperentBitmap, 0, 0, new Rect(0, 0, 620, 300), 1.0f);
            }

            //Fill the rectangle with color
            clds.FillRectangle(0, 0, 620, 300, ViewModel.SelectedColor);

            if (_userBitmap != null)
            {
                var effect = new Transform2DEffect
                {
                    Source = _userBitmap,
                    InterpolationMode = CanvasImageInterpolation.HighQualityCubic,
                    TransformMatrix = ViewModel.Effect.TransformMatrix
                };

                //Render image
                clds.DrawImage(
                    effect,
                    ViewModel.X,
                    ViewModel.Y,
                    new Rect(
                        ViewModel.RectX,
                        ViewModel.RectY,
                        ViewModel.RectWidth,
                        ViewModel.RectHeight),
                    1.0f,
                    CanvasImageInterpolation.HighQualityCubic);
            }
        }

        private void Calculation()
        {
            if (_userBitmap != null)
            {
                #region Calcuation Logic

                if (_userBitmap.SizeInPixels.Width <= _userBitmap.SizeInPixels.Height)
                {
                    ViewModel.ZoomFactor = (float) 300 / _userBitmap.SizeInPixels.Height;
                }
                else
                {
                    ViewModel.ZoomFactor = (float) 620 / _userBitmap.SizeInPixels.Width;
                }

                ViewModel.X = 310 - _userBitmap.SizeInPixels.Width * ViewModel.ZoomFactor / 2;
                ViewModel.Y = 150 - _userBitmap.SizeInPixels.Height * ViewModel.ZoomFactor / 2;

                ViewModel.RectWidth = _userBitmap.SizeInPixels.Width * ViewModel.ZoomFactor;
                ViewModel.RectHeight = _userBitmap.SizeInPixels.Height * ViewModel.ZoomFactor;

                ViewModel.Effect.TransformMatrix = Matrix3x2.CreateScale(new Vector2(ViewModel.ZoomFactor));

                #endregion
            }
        }
    }
}
