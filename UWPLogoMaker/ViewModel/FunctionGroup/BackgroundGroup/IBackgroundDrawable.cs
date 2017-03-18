namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    using Windows.UI;

    public interface IBackgroundDrawable
    {
        BackgroundViewModel BackgroundVm { get; set; }

        Color CurrentColor { get; set; }

        void ChangeColor();

        void Update();
    }
}
