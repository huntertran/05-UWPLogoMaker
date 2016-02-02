using System;
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

namespace UWPLogoMaker.View.PlatformGroup
{
    public sealed partial class UwpPage
    {
        /// <summary>
        /// Gets the view's ViewModel.
        /// </summary>
        public UwpViewModel Vm => (UwpViewModel) DataContext;
        
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
            }

            await Vm.LoadBitmap();

            Vm.IsCaculation = true;
            await Vm.DisplayPreview();
            TestCanvasControl.Invalidate();
        }

        private async void GenerateLogo_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Vm.IsShowingProgress = true;
            await Vm.DoTheGenerate();
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
            await Vm.DisplayPreview();
            TestCanvasControl.Invalidate();
        }

        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            await Vm.DisplayPreview();
            TestCanvasControl.Invalidate();
        }

        private async void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            await Vm.DisplayPreview();
            TestCanvasControl.Invalidate();
        }

        private async void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            await Vm.DisplayPreview();
            TestCanvasControl.Invalidate();
        }

        private void Zoom_ValueChanged(object sender,
            Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
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
    }
}