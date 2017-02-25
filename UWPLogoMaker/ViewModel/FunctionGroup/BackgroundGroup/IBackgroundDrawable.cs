using Windows.UI;

namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    public interface IBackgroundDrawable
    {
        BackgroundViewModel BackgroundVm { get; set; }

        Color CurrentColor { get; set; }

        void ChangeColor();

        void Update();
    }
}
