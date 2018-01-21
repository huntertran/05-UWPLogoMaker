using System;

using UniversalLogoMaker.ViewModels;

using Windows.UI.Xaml.Controls;

namespace UniversalLogoMaker.Views
{
    public sealed partial class GeneratePage : Page
    {
        public GenerateViewModel ViewModel { get; } = new GenerateViewModel();

        public GeneratePage()
        {
            InitializeComponent();
        }
    }
}
