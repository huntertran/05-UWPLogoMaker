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
    }
}
