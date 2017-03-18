﻿namespace UWPLogoMaker.View.FunctionGroup.BackgroundGroup
{
    using Windows.System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using ViewModel.FunctionGroup.BackgroundGroup;

    public sealed partial class ColorPage
    {
        public ColorBackgroundViewModel Vm => (ColorBackgroundViewModel)DataContext;

        public ColorPage()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Vm.Update();
        }
            
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Vm.Update();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Vm.Update();
        }
        
        private void HexaCodeTextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                ATextBox.Focus(FocusState.Pointer);
            }
        }
    }
}
