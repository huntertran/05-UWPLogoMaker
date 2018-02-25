namespace UniversalLogoMaker3.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Helpers;
    using Microsoft.Toolkit.Uwp.UI.Controls;
    using Services;
    using Views;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Markup;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Navigation;
    using Windows.UI.Xaml.Shapes;
    using Models;

    public class ShellViewModel : Observable
    {
        private const string PanoramicStateName = "PanoramicState";
        private const string WideStateName = "WideState";
        private const string NarrowStateName = "NarrowState";
        private const double WideStateMinWindowWidth = 640;
        private const double PanoramicStateMinWindowWidth = 1024;

        private bool _isPaneOpen;

        public bool IsPaneOpen
        {
            get => _isPaneOpen;
            set => Set(ref _isPaneOpen, value);
        }

        private object _selected;

        public object Selected
        {
            get => _selected;
            set => Set(ref _selected, value);
        }

        private SplitViewDisplayMode _displayMode = SplitViewDisplayMode.CompactInline;

        public SplitViewDisplayMode DisplayMode
        {
            get => _displayMode;
            set => Set(ref _displayMode, value);
        }

        private object _lastSelectedItem;

        private ObservableCollection<ShellNavigationItem> _primaryItems =
            new ObservableCollection<ShellNavigationItem>();

        public ObservableCollection<ShellNavigationItem> PrimaryItems
        {
            get => _primaryItems;
            set => Set(ref _primaryItems, value);
        }

        private ObservableCollection<ShellNavigationItem> _secondaryItems =
            new ObservableCollection<ShellNavigationItem>();

        public ObservableCollection<ShellNavigationItem> SecondaryItems
        {
            get => _secondaryItems;
            set => Set(ref _secondaryItems, value);
        }

        private ICommand _openPaneCommand;

        public ICommand OpenPaneCommand
        {
            get { return _openPaneCommand ?? (_openPaneCommand = new RelayCommand(() => IsPaneOpen = !_isPaneOpen)); }
        }

        private ICommand _itemSelected;

        public ICommand ItemSelectedCommand => _itemSelected ??
                                               (_itemSelected = new RelayCommand<HamburgetMenuItemInvokedEventArgs>(ItemSelected));

        private ICommand _stateChangedCommand;

        public ICommand StateChangedCommand
        {
            get
            {
                return _stateChangedCommand ?? (_stateChangedCommand =
                           new RelayCommand<VisualStateChangedEventArgs>(args =>
                               GoToState(args.NewState.Name)));
            }
        }

        private void InitializeState(double windowWith)
        {
            if (windowWith < WideStateMinWindowWidth)
            {
                GoToState(NarrowStateName);
            }
            else if (windowWith < PanoramicStateMinWindowWidth)
            {
                GoToState(WideStateName);
            }
            else
            {
                GoToState(PanoramicStateName);
            }
        }

        private void GoToState(string stateName)
        {
            switch (stateName)
            {
                case PanoramicStateName:
                    DisplayMode = SplitViewDisplayMode.CompactInline;
                    break;
                case WideStateName:
                    DisplayMode = SplitViewDisplayMode.CompactInline;
                    IsPaneOpen = false;
                    break;
                case NarrowStateName:
                    DisplayMode = SplitViewDisplayMode.Overlay;
                    IsPaneOpen = false;
                    break;
                default:
                    break;
            }
        }

        public async void Initialize(Frame frame)
        {
            NavigationService.Frame = frame;
            NavigationService.Navigated += Frame_Navigated;
            PopulateNavItems();

            InitializeState(Window.Current.Bounds.Width);
            await GetData();
        }

        private async Task GetData()
        {
            var data = await StorageService.Json2Object<Database>("data.dat") ?? new Database
            {
                PlatformList = new ObservableCollection<Platform>()
            };

            if (data.PlatformList.Count == 0)
            {
                InitializeData(data);
                await StorageService.Object2Json(data, "data.dat");
            }
        }

        private void InitializeData(Database data)
        {
            /////////UWP//////////

            #region UWP

            data.PlatformList = new ObservableCollection<Platform>();
            Platform p = new Platform
            {
                Name = "UWP",
                Icon = "W10",
                SaveLogoList = new List<LogoObject>(),
                IsEnabled = false
            };
            string fName = "Square71x71Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 400, 71, true));
            p.SaveLogoList.Add(new LogoObject(fName, 200, 71, true));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 71, true));
            p.SaveLogoList.Add(new LogoObject(fName, 125, 71, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 71, true));
            //
            fName = "Square150x150Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 400, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 200, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 125, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 150, true));
            //
            fName = "Square310x310Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 400, 310, true));
            p.SaveLogoList.Add(new LogoObject(fName, 200, 310, true));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 310, true));
            p.SaveLogoList.Add(new LogoObject(fName, 125, 310, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 310, true));
            //
            fName = "Square44x44Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 400, 44, true));
            p.SaveLogoList.Add(new LogoObject(fName, 200, 44, true));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 44, true));
            p.SaveLogoList.Add(new LogoObject(fName, 125, 44, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 44, true));

            //
            fName = "Target";
            p.SaveLogoList.Add(new LogoObject(fName, 100, 16, true));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 16, true));
            p.SaveLogoList.Add(new LogoObject(fName, 300, 16, true));
            p.SaveLogoList.Add(new LogoObject(fName, 1600, 16, true));
            //
            fName = "StoreLogo";
            p.SaveLogoList.Add(new LogoObject(fName, 400, 50, true));
            p.SaveLogoList.Add(new LogoObject(fName, 200, 50, true));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 50, true));
            p.SaveLogoList.Add(new LogoObject(fName, 125, 50, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 50, true));
            //
            fName = "Wide310x150Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 400, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 200, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 125, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 310, false));

            fName = "SplashScreen";
            p.SaveLogoList.Add(new LogoObject(fName, 400, 620, false));
            p.SaveLogoList.Add(new LogoObject(fName, 200, 620, false));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 620, false));
            p.SaveLogoList.Add(new LogoObject(fName, 125, 620, false));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 620, false));

            data.PlatformList.Add(p);

            #endregion

            /////////Windows 8.1//////////////////

            #region Windows 8.1

            p = new Platform
            {
                Name = "Windows 8.1",
                Icon = "W8.1",
                SaveLogoList = new List<LogoObject>()
            };
            fName = "Square70x70Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 70, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 70, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 70, true));
            p.SaveLogoList.Add(new LogoObject(fName, 80, 70, true));
            //
            fName = "Square150x150Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 80, 150, true));
            //
            fName = "Square310x310Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 310, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 310, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 310, true));
            p.SaveLogoList.Add(new LogoObject(fName, 80, 310, true));
            //
            fName = "Square30x30Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 30, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 30, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 30, true));
            p.SaveLogoList.Add(new LogoObject(fName, 80, 30, true));
            //
            fName = "StoreLogo";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 90, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 70, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 50, true));
            //
            fName = "Wide310x150Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 80, 310, false));
            //
            fName = "BadgeLogo";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 24, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 24, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 24, true));

            fName = "SplashScreen";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 620, false, "620:300"));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 620, false, "620:300"));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 620, false, "620:300"));

            data.PlatformList.Add(p);

            #endregion

            /////////Windows Phone 8.1////////////

            #region Windows Phone 8.1

            p = new Platform
            {
                Name = "Windows Phone 8.1",
                Icon = "WP8.1",
                SaveLogoList = new List<LogoObject>()
            };
            fName = "Square71x71Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 240, 71, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 71, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 71, true));

            fName = "Square150x150Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 240, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 150, true));

            fName = "Square44x44Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 240, 44, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 44, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 44, true));

            fName = "StoreLogo";
            p.SaveLogoList.Add(new LogoObject(fName, 240, 50, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 50, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 50, true));

            fName = "Wide310×150Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 240, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 310, false));

            fName = "SplashScreen";
            p.SaveLogoList.Add(new LogoObject(fName, 240, 480, false, "480:800"));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 480, false, "480:800"));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 480, false, "480:800"));

            data.PlatformList.Add(p);

            data.DatabaseVersion = 0;
            data.UpdateMessage = "Welcome";

            #endregion
        }

        private void PopulateNavItems()
        {
            _primaryItems.Clear();
            _secondaryItems.Clear();

            // TODO WTS: Change the symbols for each item as appropriate for your app
            // More on Segoe UI Symbol icons: https://docs.microsoft.com/windows/uwp/style/segoe-ui-symbol-font
            // Or to use an IconElement instead of a Symbol see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/projectTypes/navigationpane.md
            // Edit String/en-US/Resources.resw: Add a menu item title for each page

            _primaryItems.Add(ShellNavigationItem.FromType<MainPage>(
                "Shell_Main".GetLocalized(),
                new PathIcon
                {
                    Data = PathMarkupToGeometry("M2,22.414024L2,30.000023 9.5859995,30.000023z M30,20.000023L32,20.000023 32,32.000023 20,32.000023 20,30.000023 30,30.000023z M22.41397,1.9999995L29.999968,9.5859974 29.999968,1.9999995z M0,2.28882E-05L12,2.28882E-05 12,2.000023 2,2.000023 2,12.000023 0,12.000023z M17.585968,0L31.999968,0 31.999968,14.413999 25.50001,7.9140418 7.9139872,25.500011 14.413999,32.000023 0,32.000023 0,17.586025 6.4999857,24.08601 24.086008,6.5000399z")
                }));

            _primaryItems.Add(ShellNavigationItem.FromType<AddSizePage>(
                "Shell_AddSize".GetLocalized(),
                new PathIcon
                {
                    Data = PathMarkupToGeometry(
                        "M10.830011,12.05L13.334007,12.05 13.334007,16.535997 17.819998,16.535997 17.819998,19.03902 13.334007,19.03902 13.334007,23.525016 10.830988,23.525016 10.830988,19.03902 6.3460046,19.03902 6.3449975,16.53499 10.830988,16.535997z M2.2799975,2.4149805L2.2799975,29.580015 22.121998,29.580015 22.121998,8.500008 15.83599,8.500008 15.83599,2.4149805z M0,0L15.83599,0 17.125998,0 22.681996,5.439977 24.407,7.125007 24.407,32.000001 0,32.000001z")
                }));

            _primaryItems.Add(ShellNavigationItem.FromType<AboutPage>(
                "Shell_About".GetLocalized(),
                new PathIcon
                {
                    Data = PathMarkupToGeometry("M16.200012,11.899994C15.5,12.300003 14.799988,12.600006 14.299988,12.800003 13.700012,13 13,13.100006 12.200012,13.199997L12.200012,14.5 14.799988,14.5 14.799988,23.399994C14.799988,24.199997 14.799988,24.699997 14.700012,25 14.600037,25.199997 14.400024,25.399994 14.200012,25.5 14,25.600006 13.5,25.699997 12.799988,25.699997L12.200012,25.699997 12.200012,27 20,27 20,25.699997 19.299988,25.699997C18.700012,25.699997 18.299988,25.699997 18.100037,25.600006 17.900024,25.5 17.700012,25.300003 17.600037,25.100006 17.5,24.899994 17.400024,24.600006 17.400024,24.100006L17.400024,11.899994z M16,5.1000061C15.5,5.1000061 15.100037,5.3000031 14.799988,5.6000061 14.5,5.8999939 14.299988,6.3999939 14.299988,6.8000031 14.299988,7.3000031 14.5,7.6999969 14.799988,8 15.100037,8.3000031 15.600037,8.5 16,8.5 16.5,8.5 16.900024,8.3000031 17.200012,8 17.5,7.6999969 17.700012,7.1999969 17.700012,6.8000031 17.700012,6.3000031 17.5,5.8999939 17.200012,5.6000061 16.900024,5.3000031 16.5,5.1000061 16,5.1000061z M16,0C24.799988,0 32,7.1999969 32,16 32,24.800003 24.799988,32 16,32 7.2000122,32 0,24.800003 0,16 0,7.1999969 7.2000122,0 16,0z")
                }));

            _secondaryItems.Add(ShellNavigationItem.FromType<SettingsPage>(
                "Shell_Settings".GetLocalized(),
                Symbol.Setting));
        }

        private static Geometry PathMarkupToGeometry(string pathMarkup)
        {
            var xaml =
                $"<Path xmlns=\'http://schemas.microsoft.com/winfx/2006/xaml/presentation\'><Path.Data>{pathMarkup}</Path.Data></Path>";

            var path = (Path)XamlReader.Load(xaml);

            // Detach the PathGeometry from the Path
            if (path != null)
            {
                var geometry = path.Data;
                path.Data = null;

                return geometry;
            }

            return null;
        }

        private void ItemSelected(HamburgetMenuItemInvokedEventArgs args)
        {
            if (DisplayMode == SplitViewDisplayMode.CompactOverlay || DisplayMode == SplitViewDisplayMode.Overlay)
            {
                IsPaneOpen = false;
            }

            Navigate(args.InvokedItem);
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            var navigationItem = PrimaryItems?.FirstOrDefault(i => i.PageType == e?.SourcePageType) ?? SecondaryItems?.FirstOrDefault(i => i.PageType == e?.SourcePageType);

            if (navigationItem != null)
            {
                ChangeSelected(_lastSelectedItem, navigationItem);
                _lastSelectedItem = navigationItem;
            }
        }

        private void ChangeSelected(object oldValue, object newValue)
        {
            if (oldValue != null)
            {
                ((ShellNavigationItem)oldValue).IsSelected = false;
            }

            if (newValue != null)
            {
                ((ShellNavigationItem)newValue).IsSelected = true;
                Selected = newValue;
            }
        }

        private static void Navigate(object item)
        {
            var navigationItem = item as ShellNavigationItem;
            if (navigationItem != null)
            {
                NavigationService.Navigate(navigationItem.PageType);
            }
        }
    }
}
