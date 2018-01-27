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
    using Windows.UI.Xaml.Media;

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
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, object e)
        {
            if (ViewModel != null)
            {
                UpdateValues();
            }
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
                    //TransformMatrix = ViewModel.Effect.TransformMatrix
                    TransformMatrix = Matrix3x2.CreateScale(new Vector2(ViewModel.ZoomFactor))
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
                    ViewModel.ZoomFactor = (float)300 / _userBitmap.SizeInPixels.Height;
                }
                else
                {
                    ViewModel.ZoomFactor = (float)620 / _userBitmap.SizeInPixels.Width;
                }

                ViewModel.X = 310 - _userBitmap.SizeInPixels.Width * ViewModel.ZoomFactor / 2;
                ViewModel.Y = 150 - _userBitmap.SizeInPixels.Height * ViewModel.ZoomFactor / 2;

                ViewModel.RectWidth = _userBitmap.SizeInPixels.Width * ViewModel.ZoomFactor;
                ViewModel.RectHeight = _userBitmap.SizeInPixels.Height * ViewModel.ZoomFactor;

                ViewModel.MaxWidth = _userBitmap.SizeInPixels.Width;
                ViewModel.MaxHeight = _userBitmap.SizeInPixels.Height;

                XPos.Maximum = ViewModel.MaxWidth;
                YPos.Maximum = ViewModel.MaxHeight;

                XPos.Minimum = ViewModel.MaxWidth * -1;
                YPos.Minimum = ViewModel.MaxHeight * -1;

                ViewModel.Effect.TransformMatrix = Matrix3x2.CreateScale(new Vector2(ViewModel.ZoomFactor));

                #endregion
            }
        }

        private void UpdateValues()
        {
            if (_userBitmap != null)
            {
                float x;
                float y;

                if (ViewModel.RectWidth > 0 && ViewModel.RectHeight > 0)
                {
                    x = (float) (310 - ViewModel.RectWidth / 2);
                    y = (float) (150 - ViewModel.RectHeight / 2);
                    XPos.Maximum = ViewModel.RectWidth + 2 * x;
                    YPos.Maximum = ViewModel.RectHeight + 2 * y;

                    XPos.Minimum = ViewModel.RectWidth * -1;
                    YPos.Minimum = ViewModel.RectHeight * -1;
                }
                else if (ViewModel.MaxWidth > 0 && ViewModel.MaxHeight > 0)
                {
                    //ViewModel.ZoomFactor is Zoom * 100, so...
                    x = (float) (310 - ViewModel.MaxWidth * ViewModel.ZoomFactor / 200);
                    y = (float) (150 - ViewModel.MaxWidth * ViewModel.ZoomFactor / 200);
                    XPos.Maximum = ViewModel.MaxWidth * ViewModel.ZoomFactor / 100 + 2 * x;
                    YPos.Maximum = ViewModel.MaxHeight * ViewModel.ZoomFactor / 100 + 2 * y;

                    XPos.Minimum = ViewModel.MaxWidth * ViewModel.ZoomFactor / 100 * -1;
                    YPos.Minimum = ViewModel.MaxHeight * ViewModel.ZoomFactor / 100 * -1;
                }

                ViewModel.RectWidth = _userBitmap.SizeInPixels.Width * ViewModel.ZoomFactor;
                ViewModel.RectHeight = _userBitmap.SizeInPixels.Height * ViewModel.ZoomFactor;
            }
        }
    }
}
