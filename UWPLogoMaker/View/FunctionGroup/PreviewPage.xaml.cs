using System;
using System.Diagnostics;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Graphics.Canvas.UI.Xaml;
using UWPLogoMaker.ViewModel.PlatformGroup;

namespace UWPLogoMaker.View.FunctionGroup
{
    public sealed partial class PreviewPage
    {
        /// <summary>
        /// Gets the view's ViewModel.
        /// </summary>
        public UwpViewModel Vm => (UwpViewModel) DataContext;
        
        public PreviewPage()
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

                XPos.Minimum = Vm.MaxWidth*(-1);
                YPos.Minimum = Vm.MaxHeight*(-1);

                //Square
                Vm.SMaxWidth = bm.PixelWidth;
                Vm.SMaxHeight = bm.PixelHeight;

                SqXPos.Maximum = Vm.SMaxWidth;
                SqYPos.Maximum = Vm.SMaxHeight;

                SqXPos.Minimum = Vm.SMaxWidth * (-1);
                SqYPos.Minimum = Vm.SMaxHeight * (-1);
            }

            await Vm.LoadBitmap();

            Vm.IsCaculation = true;
            Vm.SIsCaculation = true;
            Vm.DisplayPreview();
            Vm.DisplaySquarePreview();
            WideCanvasControl.Invalidate();
            SquareCanvasControl.Invalidate();
        }

        private async void GenerateLogo_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Vm.IsShowingProgress = true;
            //await Vm.DoTheGenerate();
            await Vm.DoTheGenerateWin2DTask();
        }

        private void HexaCodeTextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                ATextBox.Focus(FocusState.Pointer);
            }
        }

        private void WideCanvasControl_OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (Vm.RenderTarget != null)
            {
                args.DrawingSession.DrawImage(Vm.RenderTarget);
            }
        }

        private void SquareCanvasControl_OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (Vm.SRenderTarget != null)
            {
                args.DrawingSession.DrawImage(Vm.SRenderTarget);
            }
        }

        private void DrawButton_OnClick(object sender, RoutedEventArgs e)
        {
            Vm.IsCaculation = true;
            Vm.DisplayPreview();
            WideCanvasControl.Invalidate();

            Debug.Assert(LinkCheckBox.IsChecked != null, "LinkCheckBox.IsChecked != null");
            if (!(bool) LinkCheckBox.IsChecked)
            {
                Vm.SIsCaculation = true;
                Vm.DisplaySquarePreview();
                SquareCanvasControl.Invalidate();
            }
        }

        private void SquareDrawButton_OnClick(object sender, RoutedEventArgs e)
        {
            Vm.SIsCaculation = true;
            Vm.DisplaySquarePreview();
            SquareCanvasControl.Invalidate();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Vm.DisplayPreview();
            WideCanvasControl.Invalidate();

            Vm.DisplaySquarePreview();
            SquareCanvasControl.Invalidate();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Vm.DisplayPreview();
            WideCanvasControl.Invalidate();
            
            Vm.DisplaySquarePreview();
            SquareCanvasControl.Invalidate();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Vm.DisplayPreview();
            WideCanvasControl.Invalidate();

            Vm.DisplaySquarePreview();
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

            if (Vm.RecW > 0 && Vm.RecH > 0)
            {
                x = (float) (310 - Vm.RecW/2);
                y = (float) (150 - Vm.RecH/2);
                XPos.Maximum = Vm.RecW + (2*x);
                YPos.Maximum = Vm.RecH + (2*y);

                XPos.Minimum = Vm.RecW*(-1);
                YPos.Minimum = Vm.RecH*(-1);
            }
            else if (Vm.MaxWidth > 0 && Vm.MaxHeight > 0)
            {
                //e.NewValue is Zoom * 100, so...
                x = (float) (310 - Vm.MaxWidth*e.NewValue/200);
                y = (float) (150 - Vm.MaxWidth*e.NewValue/200);
                XPos.Maximum = Vm.MaxWidth*e.NewValue/100 + 2*x;
                YPos.Maximum = Vm.MaxHeight*e.NewValue / 100 + 2*y;

                XPos.Minimum = Vm.MaxWidth*e.NewValue / 100 * (-1);
                YPos.Minimum = Vm.MaxHeight*e.NewValue / 100 * (-1);
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

            if (Vm.SRecW > 0 && Vm.SRecH > 0)
            {
                x = (float)(150 - Vm.SRecW / 2);
                y = (float)(150 - Vm.SRecH / 2);
                SqXPos.Maximum = Vm.SRecW + (2 * x);
                SqYPos.Maximum = Vm.SRecH + (2 * y);

                SqXPos.Minimum = Vm.SRecW * (-1);
                SqYPos.Minimum = Vm.SRecH * (-1);
            }
            else if (Vm.SMaxWidth > 0 && Vm.SMaxHeight > 0)
            {
                //e.NewValue is Zoom * 100, so...
                x = (float)(150 - Vm.SMaxWidth * e.NewValue / 200);
                y = (float)(150 - Vm.SMaxWidth * e.NewValue / 200);

                SqXPos.Maximum = Vm.SMaxWidth * e.NewValue / 100 + 2 * x;
                SqYPos.Maximum = Vm.SMaxHeight * e.NewValue / 100 + 2 * y;

                SqXPos.Minimum = Vm.SMaxWidth * e.NewValue / 100 * (-1);
                SqYPos.Minimum = Vm.SMaxHeight * e.NewValue / 100 * (-1);
            }
        }
    }
}