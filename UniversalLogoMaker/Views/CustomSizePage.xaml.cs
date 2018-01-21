namespace UniversalLogoMaker.Views
{
    using ViewModels;

    public sealed partial class CustomSizePage
    {
        public CustomSizeViewModel ViewModel { get; } = new CustomSizeViewModel();

        public CustomSizePage()
        {
            InitializeComponent();
        }
    }
}
