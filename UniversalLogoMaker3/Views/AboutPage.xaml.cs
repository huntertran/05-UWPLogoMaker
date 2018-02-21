using System;

using UniversalLogoMaker3.ViewModels;

using Windows.UI.Xaml.Controls;

namespace UniversalLogoMaker3.Views
{
    public sealed partial class AboutPage : Page
    {
        public AboutViewModel ViewModel { get; } = new AboutViewModel();

        public AboutPage()
        {
            InitializeComponent();
            ViewModel.Initialize(webView);
        }
    }
}
