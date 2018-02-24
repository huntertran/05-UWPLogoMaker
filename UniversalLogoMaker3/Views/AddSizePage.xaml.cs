using System;

using UniversalLogoMaker3.ViewModels;

using Windows.UI.Xaml.Controls;

namespace UniversalLogoMaker3.Views
{
    public sealed partial class AddSizePage : Page
    {
        public AddSizeViewModel ViewModel { get; } = new AddSizeViewModel();

        public AddSizePage()
        {
            InitializeComponent();
        }
    }
}
