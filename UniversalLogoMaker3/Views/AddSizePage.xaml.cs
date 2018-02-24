namespace UniversalLogoMaker3.Views
{
    using ViewModels;

    public sealed partial class AddSizePage
    {
        public AddSizeViewModel ViewModel { get; } = new AddSizeViewModel();

        public AddSizePage()
        {
            InitializeComponent();
        }
    }
}
