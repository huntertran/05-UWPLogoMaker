namespace UniversalLogoMaker.Views
{
    using Windows.UI.Xaml.Input;
    using ViewModels;

    public sealed partial class GeneratePage
    {
        public GenerateViewModel ViewModel { get; } = new GenerateViewModel();

        public GeneratePage()
        {
            InitializeComponent();
        }

        private void ChooseImageButton_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
