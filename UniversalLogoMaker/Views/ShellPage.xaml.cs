namespace UniversalLogoMaker.Views
{
    using ViewModels;

    public sealed partial class ShellPage
    {
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame);
        }
    }
}
