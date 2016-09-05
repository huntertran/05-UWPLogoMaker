using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.Graphics.Canvas.Brushes;
using UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup;

namespace UWPLogoMaker.View.FunctionGroup.BackgroundGroup
{
    public sealed partial class GradientColorPage
    {
        public GradientColorBackgroundViewModel Vm => (GradientColorBackgroundViewModel)DataContext;

        public GradientColorPage()
        {
            InitializeComponent();
        }

        private void AddGradientStopButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Vm.AddGradientStop();
        }

        private void DeleteGradientStop_OnClick(object sender, RoutedEventArgs e)
        {
            CanvasGradientStop c = ((AppBarButton) sender).Tag as CanvasGradientStop? ?? new CanvasGradientStop();
            Vm.RemoveGradientStop(c);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Vm.Update();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Vm.Update();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Vm.Update();
        }

        private void HexaCodeTextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                ATextBox.Focus(FocusState.Pointer);
            }
        }
    }
}
