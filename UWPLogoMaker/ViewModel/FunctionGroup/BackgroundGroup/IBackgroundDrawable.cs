namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    using System.ComponentModel;
    using Windows.UI;
    using Windows.UI.Xaml.Media;

    public interface IBackgroundDrawable : INotifyPropertyChanged
    {
        BackgroundViewModel BackgroundVm { get; set; }

        Color CurrentColor { get; }

        Brush CurrentBrush { get; set; }

        void ChangeColor();

        void Update();
    }
}
