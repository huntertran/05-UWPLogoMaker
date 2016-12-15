using Windows.UI.Xaml.Input;

namespace UWPLogoMaker.Interfaces
{
    public interface IPreviewView
    {
        void InvalidateCanvasControl();

        void OpenImage_Tapped(object sender, TappedRoutedEventArgs e);
    }
}
