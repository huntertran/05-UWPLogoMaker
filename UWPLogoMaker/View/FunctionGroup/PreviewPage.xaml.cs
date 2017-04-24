namespace UWPLogoMaker.View.FunctionGroup
{
    using System;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media.Imaging;
    using Interfaces;
    using Microsoft.Graphics.Canvas;
    using Microsoft.Graphics.Canvas.UI;
    using Microsoft.Graphics.Canvas.UI.Xaml;
    using ViewModel.FunctionGroup;

    public sealed partial class PreviewPage : IPreviewView
    {
        public MainViewModel Vm => (MainViewModel) DataContext;
        
        public PreviewPage()
        {
            InitializeComponent();
            Vm.View = this;
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Vm.IsShowCenterLine = true;
            Vm.BackgroundVm.ColorBackgroundVm.ChangeColor();
        }

        public void InvalidateCanvasControl()
        {
            WideCanvasControl.Invalidate();
            SquareCanvasControl.Invalidate();
        }
        
        public async void OpenImage_Tapped(object sender, TappedRoutedEventArgs e)
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

            Vm.File = await openPicker.PickSingleFileAsync();

            if (Vm.File == null)
            {
                return;
            }

            using (IRandomAccessStream fileStream = await Vm.File.OpenAsync(FileAccessMode.Read))
            {
                // Set the image source to the selected bitmap
                BitmapImage bm = new BitmapImage();
                await bm.SetSourceAsync(fileStream);

                Vm.MaxWidth = bm.PixelWidth;
                Vm.MaxHeight = bm.PixelHeight;

                XPos.Maximum = Vm.MaxWidth;
                YPos.Maximum = Vm.MaxHeight;

                XPos.Minimum = Vm.MaxWidth*-1;
                YPos.Minimum = Vm.MaxHeight*-1;

                //Square
                Vm.SMaxWidth = bm.PixelWidth;
                Vm.SMaxHeight = bm.PixelHeight;

                SqXPos.Maximum = Vm.SMaxWidth;
                SqYPos.Maximum = Vm.SMaxHeight;

                SqXPos.Minimum = Vm.SMaxWidth * -1;
                SqYPos.Minimum = Vm.SMaxHeight * -1;
            }

            //TODO: load bitmap
            //await Vm.LoadBitmap();
            LoadResourceOnClick();

            //Vm.IsCaculation = true;
            //Vm.SIsCaculation = true;
            //Vm.CalculatePreview();
            //Vm.CalculateSquarePreview();
            WideCanvasControl.Invalidate();
            //SquareCanvasControl.Invalidate();
        }

        private void WideCanvasControl_OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (Vm.RenderTarget != null)
            {
                args.DrawingSession.DrawImage(Vm.RenderTarget);
                Vm.RenderTarget.Dispose();
            }
        }

        private void SquareCanvasControl_OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (Vm.SRenderTarget != null)
            {
                args.DrawingSession.DrawImage(Vm.SRenderTarget);
                Vm.SRenderTarget.Dispose();
            }
        }

        private void DrawButton_OnClick(object sender, RoutedEventArgs e)
        {
            Vm.IsCaculation = true;
            Vm.CalculatePreview();
            WideCanvasControl.Invalidate();
            
            if (!Vm.IsManualAdjustSquareImage)
            {
                Vm.SIsCaculation = true;
                Vm.CalculateSquarePreview();
                SquareCanvasControl.Invalidate();
            }
        }

        private void SquareDrawButton_OnClick(object sender, RoutedEventArgs e)
        {
            Vm.SIsCaculation = true;
            Vm.CalculateSquarePreview();
            SquareCanvasControl.Invalidate();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Vm.CalculatePreview();
            WideCanvasControl.Invalidate();

            Vm.CalculateSquarePreview();
            SquareCanvasControl.Invalidate();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Vm.CalculatePreview();
            WideCanvasControl.Invalidate();
            
            Vm.CalculateSquarePreview();
            SquareCanvasControl.Invalidate();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Vm.CalculatePreview();
            WideCanvasControl.Invalidate();

            Vm.CalculateSquarePreview();
            SquareCanvasControl.Invalidate();
        }

        private void Zoom_ValueChanged(object sender,
            Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (XPos == null || YPos == null)
            {
                return;
            }
            float x;
            float y;

            if (Vm.RectangleWidth > 0 && Vm.RectangleHeight > 0)
            {
                x = (float) (310 - Vm.RectangleWidth/2);
                y = (float) (150 - Vm.RectangleHeight/2);
                XPos.Maximum = Vm.RectangleWidth + 2*x;
                YPos.Maximum = Vm.RectangleHeight + 2*y;

                XPos.Minimum = Vm.RectangleWidth*-1;
                YPos.Minimum = Vm.RectangleHeight*-1;
            }
            else if (Vm.MaxWidth > 0 && Vm.MaxHeight > 0)
            {
                //e.NewValue is Zoom * 100, so...
                x = (float) (310 - Vm.MaxWidth*e.NewValue/200);
                y = (float) (150 - Vm.MaxWidth*e.NewValue/200);
                XPos.Maximum = Vm.MaxWidth*e.NewValue/100 + 2*x;
                YPos.Maximum = Vm.MaxHeight*e.NewValue / 100 + 2*y;

                XPos.Minimum = Vm.MaxWidth*e.NewValue / 100 * -1;
                YPos.Minimum = Vm.MaxHeight*e.NewValue / 100 * -1;
            }
        }

        private void SZoom_ValueChanged(object sender,
            Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            float x;
            float y;

            if (SqXPos == null || SqYPos == null)
            {
                return;
            }

            if (Vm.SquareRectangleWidth > 0 && Vm.SquareRectangleHeight > 0)
            {
                x = (float)(150 - Vm.SquareRectangleWidth / 2);
                y = (float)(150 - Vm.SquareRectangleHeight / 2);
                SqXPos.Maximum = Vm.SquareRectangleWidth + 2 * x;
                SqYPos.Maximum = Vm.SquareRectangleHeight + 2 * y;

                SqXPos.Minimum = Vm.SquareRectangleWidth * -1;
                SqYPos.Minimum = Vm.SquareRectangleHeight * -1;
            }
            else if (Vm.SMaxWidth > 0 && Vm.SMaxHeight > 0)
            {
                //e.NewValue is Zoom * 100, so...
                x = (float)(150 - Vm.SMaxWidth * e.NewValue / 200);
                y = (float)(150 - Vm.SMaxWidth * e.NewValue / 200);

                SqXPos.Maximum = Vm.SMaxWidth * e.NewValue / 100 + 2 * x;
                SqYPos.Maximum = Vm.SMaxHeight * e.NewValue / 100 + 2 * y;

                SqXPos.Minimum = Vm.SMaxWidth * e.NewValue / 100 * -1;
                SqYPos.Minimum = Vm.SMaxHeight * e.NewValue / 100 * -1;
            }
        }

        private Task _resourceLoadTask;

        private async Task LoadResourceAsync(CanvasControl sender)
        {
            if (Vm.File != null)
            {
                using (IRandomAccessStream fileStream = await Vm.File.OpenAsync(FileAccessMode.Read))
                {
                    //User Bitmap
                    Vm.UserBitmap = await CanvasBitmap.LoadAsync(sender, fileStream);
                }
            }
        }

        public void LoadResourceOnClick()
        {
            _resourceLoadTask = LoadResourceAsync(WideCanvasControl);
        }

        private void WideCanvasControl_OnCreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            Vm.RenderTarget = new CanvasRenderTarget(sender, 620, 300, 96);

            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        private async Task CreateResourcesAsync(CanvasControl sender)
        {
            // If there is a previous load in progress, stop it, and
            // swallow any stale errors. This implements requirement #3.
            if (_resourceLoadTask != null)
            {
                _resourceLoadTask.AsAsyncAction().Cancel();
                try { await _resourceLoadTask; } catch { }
                _resourceLoadTask = null;
            }

            // Unload resources used by the previous level here.

            // Asynchronous resource loading, for globally-required resources goes here:
            if (Vm.File != null)
            {
                using (IRandomAccessStream fileStream = await Vm.File.OpenAsync(FileAccessMode.Read))
                {
                    //User Bitmap
                    Vm.UserBitmap = await CanvasBitmap.LoadAsync(sender, fileStream);
                }
            }
        }

        bool IsLoadInProgress()
        {
            // No loading task?
            if (_resourceLoadTask == null)
                return false;

            // Loading task is still running?
            if (!_resourceLoadTask.IsCompleted)
                return true;

            // Query the load task results and re-throw any exceptions
            // so Win2D can see them. This implements requirement #2.
            try
            {
                _resourceLoadTask.Wait();
            }
            catch (AggregateException aggregateException)
            {
                // .NET async tasks wrap all errors in an AggregateException.
                // We unpack this so Win2D can directly see any lost device errors.
                aggregateException.Handle(exception => throw exception);
            }
            finally
            {
                _resourceLoadTask = null;
            }

            return false;
        }

        void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (IsLoadInProgress())
            {
                //DrawLoadingScreen();
            }
            else
            {
                Vm.IsCaculation = true;
                Vm.CalculatePreview();
                if (Vm.RenderTarget != null)
                {
                    args.DrawingSession.DrawImage(Vm.RenderTarget);
                    //Vm.RenderTarget.Dispose();
                }
            }
        }
    }
}