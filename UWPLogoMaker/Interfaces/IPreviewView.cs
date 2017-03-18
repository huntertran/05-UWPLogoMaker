namespace UWPLogoMaker.Interfaces
{
    using Windows.UI.Xaml.Input;

    public interface IPreviewView
    {
        void InvalidateCanvasControl();

        void OpenImage_Tapped(object sender, TappedRoutedEventArgs e);
    }
}
