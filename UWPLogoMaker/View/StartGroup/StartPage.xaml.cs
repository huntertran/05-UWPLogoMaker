namespace UWPLogoMaker.View.StartGroup
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using FunctionGroup;
    using Microsoft.Advertising.WinRT.UI;
    using Model;
    using Utilities;
    using ViewModel.StartGroup;

    public sealed partial class StartPage
    {
        public StartViewModel Vm => (StartViewModel)DataContext;

        private Type _currentFrame = typeof(MainPage);

        public StartPage()
        {
            InitializeComponent();
            InitAd();

            Loaded += StartPage_Loaded;
        }

        private async void StartPage_Loaded(object sender, RoutedEventArgs e)
        {
            await Vm.Initialize();
            await Task.Run(StaticMethod.CheckForDatabase);

            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(TitleGrid);
            FunctionsListView.SelectedIndex = 0;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void FunctionsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FunctionsListView.SelectedIndex != -1)
            {
                BottomListView.SelectedIndex = -1;
                MenuListItem m = FunctionsListView.SelectedItem as MenuListItem;

                var n = Vm.TopFunctionList.Where((a) => a.PageType == _currentFrame);
                var menuListItems = n as MenuListItem[] ?? n.ToArray();
                if (!menuListItems.Any())
                {
                    menuListItems = Vm.BottomFunctionList.Where((a) => a.PageType == _currentFrame).ToArray();
                }
                var currentMenu = menuListItems[0];
                currentMenu.IsEnabled = false;
                if (m != null)
                {
                    _currentFrame = m.PageType;
                    m.IsEnabled = true;
                    MainFrame.Navigate(m.PageType);
                }
                MainSplitView.IsPaneOpen = false;
            }
        }

        private void BottomFunctionsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BottomListView.SelectedIndex != -1)
            {
                FunctionsListView.SelectedIndex = -1;
                MenuListItem m = BottomListView.SelectedItem as MenuListItem;
                var n = Vm.TopFunctionList.Where((a) => a.PageType == _currentFrame);
                var menuListItems = n as MenuListItem[] ?? n.ToArray();
                if (!menuListItems.Any())
                {
                    menuListItems = Vm.BottomFunctionList.Where((a) => a.PageType == _currentFrame).ToArray();
                }
                var currentMenu = menuListItems[0];
                currentMenu.IsEnabled = false;
                if (m != null)
                {
                    _currentFrame = m.PageType;
                    m.IsEnabled = true;
                    
                    MainFrame.Navigate(m.PageType);
                }
            }
        }

        #region Advertisement

        private const int AdWidth = 300;
        private const int AdHeight = 600;
        private const int HouseAdWeight = 0;
        private const int AdRefreshSeconds = 30;
        private const int MaxErrorsPerRefresh = 3;
        private const string Wapplicationid = "c81c8e77-e646-4120-8f62-bd07a1558981";
        private const string WadunitidPaid = "11546874";
        private const string WadunitidHouse = "11546875";
        private const string AdduplexAppkey = "19fbe7c0-0d9e-4b50-b9ab-806d1cc9d97c";
        private const string AdduplexAdunit = "171495";


        // Dispatch timer to fire at each ad refresh interval.
        private readonly DispatcherTimer _myAdRefreshTimer = new DispatcherTimer();

        // Global variables used for mediation decisions.
        private readonly Random _randomGenerator = new Random();
        private int _errorCountCurrentRefresh;  // Prevents infinite redirects.
        private int _adDuplexWeight;            // Will be set by GetAdDuplexWeight().

        // Declare the Microsoft and AdDuplex controls for banner ads.
        private AdControl _myMicrosoftBanner;
        private AdDuplex.AdControl _myAdDuplexBanner;

        private void InitAd()
        {
            MyAdGrid.Width = AdWidth;
            MyAdGrid.Height = AdHeight;
            _adDuplexWeight = GetAdDuplexWeight();
            RefreshBanner();

            // Start the timer to refresh the banner at the desired interval.
            _myAdRefreshTimer.Interval = new TimeSpan(0, 0, AdRefreshSeconds);
            _myAdRefreshTimer.Tick += myAdRefreshTimer_Tick;
            _myAdRefreshTimer.Start();
        }

        private int GetAdDuplexWeight()
        {
            // TODO: Change this logic to fit your needs.
            // This example uses Microsoft ads first in Canada and Mexico, then
            // AdDuplex as fallback. In France, AdDuplex is first. In other regions,
            // this example uses a weighted average approach, with 50% to AdDuplex.

            int returnValue = 20;
            //switch (GlobalizationPreferences.HomeGeographicRegion)
            //{
            //    case "CA":
            //    case "MX":
            //        returnValue = 0;
            //        break;
            //    case "FR":
            //        returnValue = 100;
            //        break;
            //    default:
            //        returnValue = 50;
            //        break;
            //}
            return returnValue;
        }

        private void ActivateMicrosoftBanner()
        {
            // Return if you hit the error limit for this refresh interval.
            if (_errorCountCurrentRefresh >= MaxErrorsPerRefresh)
            {
                MyAdGrid.Visibility = Visibility.Collapsed;
                return;
            }

            // Use random number generator and house ads weight to determine whether
            // to use paid ads or house ads. Paid is the default. You could alternatively
            // write a method similar to GetAdDuplexWeight and override by region.
            string myAdUnit = WadunitidPaid;
            int houseWeight = HouseAdWeight;
            int randomInt = _randomGenerator.Next(0, 100);
            if (randomInt < houseWeight)
            {
                myAdUnit = WadunitidHouse;
            }

            // Hide the AdDuplex control if it is showing.
            if (null != _myAdDuplexBanner)
            {
                _myAdDuplexBanner.Visibility = Visibility.Collapsed;
            }

            // Initialize or display the Microsoft control.
            if (null == _myMicrosoftBanner)
            {
                _myMicrosoftBanner = new AdControl
                {
                    ApplicationId = Wapplicationid,
                    AdUnitId = myAdUnit,
                    Width = AdWidth,
                    Height = AdHeight,
                    IsAutoRefreshEnabled = false
                };

                _myMicrosoftBanner.AdRefreshed += myMicrosoftBanner_AdRefreshed;
                _myMicrosoftBanner.ErrorOccurred += myMicrosoftBanner_ErrorOccurred;

                MyAdGrid.Children.Add(_myMicrosoftBanner);
            }
            else
            {
                _myMicrosoftBanner.AdUnitId = myAdUnit;
                _myMicrosoftBanner.Visibility = Visibility.Visible;
                _myMicrosoftBanner.Refresh();
            }
        }

        private void ActivateAdDuplexBanner()
        {
            // Return if you hit the error limit for this refresh interval.
            if (_errorCountCurrentRefresh >= MaxErrorsPerRefresh)
            {
                MyAdGrid.Visibility = Visibility.Collapsed;
                return;
            }

            // Hide the Microsoft control if it is showing.
            if (null != _myMicrosoftBanner)
            {
                _myMicrosoftBanner.Visibility = Visibility.Collapsed;
            }

            // Initialize or display the AdDuplex control.
            if (null == _myAdDuplexBanner)
            {
                _myAdDuplexBanner = new AdDuplex.AdControl
                {
                    AppKey = AdduplexAppkey,
                    AdUnitId = AdduplexAdunit,
                    Width = AdWidth,
                    Height = AdHeight,
                    RefreshInterval = AdRefreshSeconds
                };

                _myAdDuplexBanner.AdLoaded += myAdDuplexBanner_AdLoaded;
                _myAdDuplexBanner.AdCovered += myAdDuplexBanner_AdCovered;
                _myAdDuplexBanner.AdLoadingError += myAdDuplexBanner_AdLoadingError;
                _myAdDuplexBanner.NoAd += myAdDuplexBanner_NoAd;

                MyAdGrid.Children.Add(_myAdDuplexBanner);
            }
            _myAdDuplexBanner.Visibility = Visibility.Visible;
        }

        private void myAdRefreshTimer_Tick(object sender, object e)
        {
            if (MyAdGrid.Visibility == Visibility.Visible) RefreshBanner();
        }

        private void RefreshBanner()
        {
            // Reset the error counter for this refresh interval and
            // make sure the ad grid is visible.
            _errorCountCurrentRefresh = 0;
            MyAdGrid.Visibility = Visibility.Visible;

            // Display ad from AdDuplex.
            if (100 == _adDuplexWeight)
            {
                ActivateAdDuplexBanner();
            }
            // Display Microsoft ad.
            else if (0 == _adDuplexWeight)
            {
                ActivateMicrosoftBanner();
            }
            // Use weighted approach.
            else
            {
                int randomInt = _randomGenerator.Next(0, 100);
                if (randomInt < _adDuplexWeight) ActivateAdDuplexBanner();
                else ActivateMicrosoftBanner();
            }
        }

        private void myMicrosoftBanner_AdRefreshed(object sender, RoutedEventArgs e)
        {
            // Add your code here as necessary.
        }

        private void myMicrosoftBanner_ErrorOccurred(object sender, AdErrorEventArgs e)
        {
            _errorCountCurrentRefresh++;
            ActivateAdDuplexBanner();
        }

        private void myAdDuplexBanner_AdLoaded(object sender, AdDuplex.Banners.Models.BannerAdLoadedEventArgs e)
        {
            // Add your code here as necessary.
        }

        private void myAdDuplexBanner_NoAd(object sender, AdDuplex.Common.Models.NoAdEventArgs e)
        {
            _errorCountCurrentRefresh++;
            ActivateMicrosoftBanner();
        }

        private void myAdDuplexBanner_AdLoadingError(object sender, AdDuplex.Common.Models.AdLoadingErrorEventArgs e)
        {
            _errorCountCurrentRefresh++;
            ActivateMicrosoftBanner();
        }

        private void myAdDuplexBanner_AdCovered(object sender, AdDuplex.Banners.Core.AdCoveredEventArgs e)
        {
            _errorCountCurrentRefresh++;
            ActivateMicrosoftBanner();
        }

        #endregion
    }
}