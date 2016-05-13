using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using UWPLogoMaker.Model;
using UWPLogoMaker.Utilities;
using UWPLogoMaker.View.FunctionGroup;
using UWPLogoMaker.ViewModel.StartGroup;

namespace UWPLogoMaker.View.StartGroup
{
    public sealed partial class StartPage
    {
        /// <summary>
        /// Gets the view's ViewModel.
        /// </summary>
        public StartViewModel Vm => (StartViewModel)DataContext;

        public StartPage()
        {
            InitializeComponent();
            
            Loaded += StartPage_Loaded;
        }

        private async void StartPage_Loaded(object sender, RoutedEventArgs e)
        {
            await Vm.Initialize();
            await Task.Run(StaticMethod.CheckForDatabase);

            SideAd.Visibility = Vm.Data.IsShow != 0 ? Visibility.Visible : Visibility.Collapsed;

#if DEBUG
            SideAd.Visibility = Visibility.Visible;
#endif

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
                Debug.Assert(m != null, "m != null");
                Vm.NavigateToFunction(MainFrame, m.MenuF);
                MainSplitView.IsPaneOpen = false;
            }
        }

        private void BottomFunctionsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BottomListView.SelectedIndex != -1)
            {
                FunctionsListView.SelectedIndex = -1;
                MenuListItem m = BottomListView.SelectedItem as MenuListItem;
                Debug.Assert(m != null, "m != null");
                Vm.NavigateToFunction(MainFrame, m.MenuF);
                MainSplitView.IsPaneOpen = false;
            }
        }

        private void sideAd_AdMediatorError(object sender, Microsoft.AdMediator.Core.Events.AdMediatorFailedEventArgs e)
        {
            Debug.WriteLine("Ad error:" + e.Error);
        }

        private void PlatformButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (!(MainFrame.Content is PreviewPage))
            {
                Vm.NavigateToFunction(MainFrame, MenuFunc.Uwp);
            }
        }
    }
}
