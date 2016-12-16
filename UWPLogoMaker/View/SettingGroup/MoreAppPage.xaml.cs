﻿using System;
using System.Linq;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using UWPLogoMaker.Model;
using UWPLogoMaker.ViewModel.SettingGroup;

namespace UWPLogoMaker.View.SettingGroup
{
    public sealed partial class MoreAppPage
    {
        public MoreAppViewModel Vm => (MoreAppViewModel)DataContext;

        public MoreAppPage()
        {
            InitializeComponent();
        }

        public async void MoreAppItem_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            AppItem a = ((Grid)sender).DataContext as AppItem;
            if (a != null)
                await
                    Launcher.LaunchUriAsync(
                        new Uri("ms-windows-store://pdp/?ProductId=" + a.link.Trim('/').Split('/').Last()));
        }
    }
}