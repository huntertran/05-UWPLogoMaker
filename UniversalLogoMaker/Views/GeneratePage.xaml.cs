namespace UniversalLogoMaker.Views
{
    using System;
    using System.Numerics;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Core;
    using Microsoft.Graphics.Canvas;
    using Microsoft.Graphics.Canvas.UI;
    using Microsoft.Graphics.Canvas.UI.Xaml;
    using ViewModels;
    using Windows.Foundation;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Input;
    using Microsoft.Graphics.Canvas.Effects;

    public sealed partial class GeneratePage
    {
        private CanvasDevice _device;
        private CanvasBitmap _transperentBitmap;
        private CanvasBitmap _userBitmap;

        public GeneratePage()
        {
            InitializeComponent();
        }

        public StorageFile File { get; set; }

        public CanvasBitmap UserBitmap
        {
            get => _userBitmap;
            set
            {
                if (value.Equals(_userBitmap)) return;
                _userBitmap = value;

                // Reload Effect
                ViewModel.Effect = new Transform2DEffect
                {
                    InterpolationMode = CanvasImageInterpolation.HighQualityCubic
                };
            }
        }

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

            if (UserBitmap != null)
            {
                //#region Calcuation Logic

                //if (UserBitmap.SizeInPixels.Width <= UserBitmap.SizeInPixels.Height)
                //{
                //    ViewModel.ZoomFactor = (float)300 / UserBitmap.SizeInPixels.Height;
                //}
                //else
                //{
                //    ViewModel.ZoomFactor = (float)620 / UserBitmap.SizeInPixels.Width;
                //}

                //ViewModel.X = 310 - UserBitmap.SizeInPixels.Width * ViewModel.ZoomFactor / 2;
                //ViewModel.Y = 150 - UserBitmap.SizeInPixels.Height * ViewModel.ZoomFactor / 2;

                //ViewModel.RectWidth = UserBitmap.SizeInPixels.Width * ViewModel.ZoomFactor;
                //ViewModel.RectHeight = UserBitmap.SizeInPixels.Height * ViewModel.ZoomFactor;

                var effect = new Transform2DEffect
                {
                    Source = UserBitmap,
                    InterpolationMode = CanvasImageInterpolation.HighQualityCubic,
                    TransformMatrix = Matrix3x2.CreateScale(new Vector2(ViewModel.ZoomFactor))
                };

                //#endregion

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
            WideCanvasAnimatedControl.Update += WideCanvasAnimatedControl_Update;
        }

        private void WideCanvasAnimatedControl_Update(
            ICanvasAnimatedControl sender,
            CanvasAnimatedUpdateEventArgs args)
        {
            if (UserBitmap != null)
            {
                #region Calcuation Logic

                if (UserBitmap.SizeInPixels.Width <= UserBitmap.SizeInPixels.Height)
                {
                    ViewModel.ZoomFactor = (float)300 / UserBitmap.SizeInPixels.Height;
                }
                else
                {
                    ViewModel.ZoomFactor = (float)620 / UserBitmap.SizeInPixels.Width;
                }

                ViewModel.X = 310 - UserBitmap.SizeInPixels.Width * ViewModel.ZoomFactor / 2;
                ViewModel.Y = 150 - UserBitmap.SizeInPixels.Height * ViewModel.ZoomFactor / 2;

                ViewModel.RectWidth = UserBitmap.SizeInPixels.Width * ViewModel.ZoomFactor;
                ViewModel.RectHeight = UserBitmap.SizeInPixels.Height * ViewModel.ZoomFactor;

                //var effect = new Transform2DEffect
                //{
                //    Source = UserBitmap,
                //    InterpolationMode = CanvasImageInterpolation.HighQualityCubic,
                //    TransformMatrix = Matrix3x2.CreateScale(new Vector2(ViewModel.ZoomFactor))
                //};

                #endregion
            }
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

        private void WideCanvasAnimatedControl_Draw(
            ICanvasAnimatedControl sender,
            CanvasAnimatedDrawEventArgs args)
        {
            //await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
            //    Windows.UI.Core.CoreDispatcherPriority.Normal,
            //    () =>
            //    {
                    DrawPreview(args.DrawingSession);
                //});
        }
    }
}
