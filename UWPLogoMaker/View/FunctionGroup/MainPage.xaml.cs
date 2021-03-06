﻿using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using UWPLogoMaker.Model;
using UWPLogoMaker.View.FunctionGroup.BackgroundGroup;
using UWPLogoMaker.ViewModel.FunctionGroup;

namespace UWPLogoMaker.View.FunctionGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        /// <summary>
        /// Gets the view's ViewModel.
        /// </summary>
        public MainViewModel Vm => (MainViewModel)DataContext;

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            PreviewFrame.Navigate(typeof (PreviewPage));
            MainPlatformListView.SelectedIndex = 0;
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pivot p = sender as Pivot;
            Debug.Assert(p != null, "p != null");
            switch (p.SelectedIndex)
            {
                case -1:
                    break;
                case 0:
                    //Color
                    BackgroundFrame.Navigate(typeof(ColorPage));
                    Vm.BackgroundVm.BackgroundMode = BackgroundMode.SolidColorBrush;
                    break;
                case 5:
                    //Gradient Color Brush
                    BackgroundFrame.Navigate(typeof(GradientColorPage));
                    Vm.BackgroundVm.BackgroundMode = BackgroundMode.GradientColorBrush;
                    break;
                case 1:
                    //Geometry
                    BackgroundFrame.Navigate(typeof(GeometryPage));
                    Vm.BackgroundVm.BackgroundMode = BackgroundMode.Geometry;
                    break;
            }
        }

        private async void GenerateLogo_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Vm.IsShowingProgress = true;
            await Vm.DoTheGenerateWin2DTask();
        }

        private void MainPlatformListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Platform platform in MainPlatformListView.SelectedItems)
            {
                platform.IsEnabled = true;
            }
        }

        private void CustomePlatformListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Platform platform in CustomePlatformListView.SelectedItems)
            {
                platform.IsEnabled = true;
            }
        }

    }
}
