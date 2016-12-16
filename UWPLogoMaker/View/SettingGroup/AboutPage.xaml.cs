﻿using System;
using Windows.System;
using Windows.UI.Xaml;

namespace UWPLogoMaker.View.SettingGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage
    {
        //private readonly DispatcherTimer _timer;
        //private bool _isStarted;
        //private int _times;

        public AboutPage()
        {
            InitializeComponent();
            //_timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(5000) };
            //_timer.Tick += _timer_Tick;
            //_times = 0;
        }

        //private void _timer_Tick(object sender, object e)
        //{
        //    _times = 0;
        //    _isStarted = false;
        //    _timer.Stop();
        //}

        //private void Path_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        //{
        //    if (!_isStarted)
        //    {
        //        _isStarted = true;
        //        _timer.Start();
        //    }
        //    else
        //    {
        //        _times++;
        //    }

        //    if (_times >= 5)
        //    {
        //        TuanPic.Visibility = Visibility.Visible;
        //        ThiPic.Visibility = Visibility.Visible;
        //    }
        //}

        private async void EmailThi_OnClick(object sender, RoutedEventArgs e)
        {
            await
                Launcher.LaunchUriAsync(
                    new Uri("mailto:Thi.NguyenVuHoang@studentpartner.com?subject=[LockBe]&body=Hello, "));
        }

        private async void LinkedInThi_OnClick(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://vn.linkedin.com/in/hoangthi1710"));
        }

        private async void EmailTuan_OnClick(object sender, RoutedEventArgs e)
        {
            await
                Launcher.LaunchUriAsync(
                    new Uri("mailto:cuoilennaocacban@hotmail.com?subject=[UWPLogoMaker]&body=Hello, "));
        }

        private async void LinkedInTuan_OnClick(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://vn.linkedin.com/in/tuanmsp"));
        }
    }
}