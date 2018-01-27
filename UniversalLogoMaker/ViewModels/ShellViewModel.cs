namespace UniversalLogoMaker.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;
    using Helpers;
    using Microsoft.Toolkit.Uwp.UI.Controls;
    using Services;
    using Views;

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
                                               (_itemSelected =
                                                   new RelayCommand<HamburgetMenuItemInvokedEventArgs>(ItemSelected));

        private ICommand _stateChangedCommand;

        public ICommand StateChangedCommand
        {
            get
            {
                return _stateChangedCommand ?? (_stateChangedCommand = new RelayCommand<VisualStateChangedEventArgs>(
                           args =>
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
            }
        }

        public void Initialize(Frame frame)
        {
            NavigationService.Frame = frame;
            NavigationService.Navigated += Frame_Navigated;
            PopulateNavItems();

            InitializeState(Window.Current.Bounds.Width);
        }

        private void PopulateNavItems()
        {
            _primaryItems.Clear();
            _secondaryItems.Clear();

            // TODO WTS: Change the symbols for each item as appropriate for your app
            // More on Segoe UI Symbol icons: https://docs.microsoft.com/windows/uwp/style/segoe-ui-symbol-font
            // Or to use an IconElement instead of a Symbol see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/projectTypes/navigationpane.md
            // Edit String/en-US/Resources.resw: Add a menu item title for each page
            _primaryItems.Add(ShellNavigationItem.FromType<GeneratePage>(
                "Shell_Generate".GetLocalized(),
                new FontIcon { Glyph = "\uE923" }));

            _primaryItems.Add(ShellNavigationItem.FromType<CustomSizePage>(
                "Shell_CustomSize".GetLocalized(),
                new FontIcon { Glyph = "\uE73F" }));

            _secondaryItems.Add(
                ShellNavigationItem.FromType<SettingsPage>("Shell_Settings".GetLocalized(), Symbol.Setting));
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
            var navigationItem = PrimaryItems?.FirstOrDefault(i => i.PageType == e?.SourcePageType) ??
                                 SecondaryItems?.FirstOrDefault(i => i.PageType == e?.SourcePageType);

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
            if (item is ShellNavigationItem navigationItem)
            {
                NavigationService.Navigate(navigationItem.PageType);
            }
        }
    }
}
