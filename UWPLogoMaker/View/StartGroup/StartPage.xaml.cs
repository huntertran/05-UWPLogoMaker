namespace UWPLogoMaker.View.StartGroup
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using FunctionGroup;
    using Model;
    using Utilities;
    using ViewModel.StartGroup;

    public sealed partial class StartPage
    {
        public StartViewModel Vm => (StartViewModel)DataContext;

        private Type _currentFrame = typeof(MainPage);

        private readonly int _maxAdError = 2;
        private int _adError = 0;

        public StartPage()
        {
            InitializeComponent();
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

        private void AdControl_AdRefreshed(object sender, RoutedEventArgs e)
        {

        }

        private void AdControl_ErrorOccurred(object sender, Microsoft.Advertising.WinRT.UI.AdErrorEventArgs e)
        {
            if (_adError < _maxAdError)
            {
                _adError++;
            }
            else
            {
                MyAdGrid.Visibility = Visibility.Collapsed;
            }
        }
    }
}