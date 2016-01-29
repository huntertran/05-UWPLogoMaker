using System;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.Graphics.Canvas.UI.Xaml;
using UWPLogoMaker.ViewModel.PlatformGroup;

namespace UWPLogoMaker.View.PlatformGroup
{
    public sealed partial class UwpPage
    {
        /// <summary>
        /// Gets the view's ViewModel.
        /// </summary>
        public UwpViewModel Vm => (UwpViewModel)DataContext;

        //Is image set size or not
        private bool _imageWideSizeSet;
        private bool _imageLogoSizeSet;

        public UwpPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Vm.ChangeColor();
        }

        /// <summary>
        /// Open Image and load to ViewModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OpenImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Reset size
            _imageWideSizeSet = false;
            _imageLogoSizeSet = false;

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

            XPos.Maximum = 100;
            YPos.Maximum = 100;

            Vm.IsCaculation = true;
            await Vm.DisplayPreview();
            TestCanvasControl.Invalidate();

            //if (Vm.File == null) return;

            //using (IRandomAccessStream fileStream = await Vm.File.OpenAsync(FileAccessMode.Read))
            //{
            //    // Set the image source to the selected bitmap
            //    Vm.WideImage = new BitmapImage();
            //    await Vm.WideImage.SetSourceAsync(fileStream);
            //}

            //using (IRandomAccessStream fileStream = await Vm.File.OpenAsync(FileAccessMode.Read))
            //{
            //    // Set the image source to the selected bitmap
            //    Vm.SquareImage = new BitmapImage();
            //    await Vm.SquareImage.SetSourceAsync(fileStream);
            //}

            //var stream = await Vm.File.OpenAsync(FileAccessMode.Read);
            //ImageProperties imageProperties = await Vm.File.Properties.GetImagePropertiesAsync();
            //WriteableBitmap bmpImage = BitmapFactory.New((int)imageProperties.Width, (int)imageProperties.Height);

            //// Load to image viewer
            //Vm.WideLogoImage = await bmpImage.FromStream(stream);
            //Vm.SquareLogoImage = await bmpImage.FromStream(stream);

            //stream.Dispose();
        }

        private async void GenerateLogo_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            //Reset data
            //Vm.WScrollViewer = WideImageViewer;
            //Vm.SScrollViewer = SquareImageViewer;

            Vm.IsShowingProgress = true;
            await Vm.DoTheGenerate();

        }

        private void WideLogoImage_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                if (!_imageWideSizeSet)
                {
                    //var zoomFactor =
                    //    (float)
                    //        Math.Min(WideImageViewer.ViewportWidth/WideLogoImage.ActualWidth,
                    //            WideImageViewer.ViewportHeight/WideLogoImage.ActualHeight);
                    //WideImageViewer.ChangeView(null, null, zoomFactor, false);
                    //_imageWideSizeSet = true;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void SquareLogoImage_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                if (!_imageLogoSizeSet)
                {
                    //var zoomFactor =
                    //    (float)
                    //        Math.Min(SquareImageViewer.ViewportWidth/SquareLogoImage.ActualWidth,
                    //            SquareImageViewer.ViewportHeight/SquareLogoImage.ActualHeight);
                    //SquareImageViewer.ChangeView(null, null, zoomFactor, false);
                    //_imageLogoSizeSet = true;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void LogoImageViewer_OnViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            //Vm.SScrollViewer = SquareImageViewer;
        }

        private void WideImageViewer_OnViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            //Vm.WScrollViewer = WideImageViewer;
        }

        private void HexaCodeTextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                ATextBox.Focus(FocusState.Pointer);
            }
        }

        private void TestCanvasControl_OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (Vm.RenderTarget != null)
            {
                args.DrawingSession.DrawImage(Vm.RenderTarget);
            }
        }

        private async void DrawButton_OnClick(object sender, RoutedEventArgs e)
        {
            Vm.IsCaculation = true;
            await Vm.DoTheGenerateWin2DTask();
            TestCanvasControl.Invalidate();
        }

        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            await Vm.DoTheGenerateWin2DTask();
            TestCanvasControl.Invalidate();
        }

        private async void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            await Vm.DoTheGenerateWin2DTask();
            TestCanvasControl.Invalidate();
        }

        private async void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            await Vm.DoTheGenerateWin2DTask();
            TestCanvasControl.Invalidate();
        }
    }
}