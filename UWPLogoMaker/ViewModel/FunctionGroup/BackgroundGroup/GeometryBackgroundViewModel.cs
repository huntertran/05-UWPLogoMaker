namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    using Windows.UI;

    public class GeometryBackgroundViewModel : BackgroundDrawable
    {
        public GeometryBackgroundViewModel(BackgroundViewModel backgroundViewModel) : base(backgroundViewModel)
        {
        }

        public override Color CurrentColor { get; set; }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeColor()
        {
            throw new System.NotImplementedException();
        }
    }
}
