using System;

using UniversalLogoMaker.ViewModels;

using Windows.UI.Xaml.Controls;

namespace UniversalLogoMaker.Views
{
    public sealed partial class ShellPage : Page
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
