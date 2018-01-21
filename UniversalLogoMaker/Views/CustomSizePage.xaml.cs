using System;

using UniversalLogoMaker.ViewModels;

using Windows.UI.Xaml.Controls;

namespace UniversalLogoMaker.Views
{
    public sealed partial class CustomSizePage : Page
    {
        public CustomSizeViewModel ViewModel { get; } = new CustomSizeViewModel();

        public CustomSizePage()
        {
            InitializeComponent();
        }
    }
}
