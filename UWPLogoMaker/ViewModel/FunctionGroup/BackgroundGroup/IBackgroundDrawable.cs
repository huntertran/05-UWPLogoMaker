namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    public interface IBackgroundDrawable
    {
        BackgroundViewModel BackgroundVm { get; set; }

        void Update();
    }
}
