namespace UniversalLogoMaker.ViewModels
{
    using Helpers;
    using Windows.UI;

    public class GenerateViewModel : Observable
    {
        private Color _selectedColor;

        public Color SelectedColor
        {
            get => _selectedColor;
            set => Set(ref _selectedColor, value);
        }

        public GenerateViewModel()
        {
        }
    }
}
