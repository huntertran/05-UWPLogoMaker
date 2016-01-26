using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using UWPLogoMaker.Model;
using UWPLogoMaker.Utilities;
using UWPLogoMaker.View.PlatformGroup;
using UWPLogoMaker.ViewModel.StartGroup;

namespace UWPLogoMaker.View.StartGroup
{
    public sealed partial class StartPage
    {
        private readonly StartViewModel _vm;

        public StartPage()
        {
            InitializeComponent();
            _vm = DataContext as StartViewModel;
            
            Loaded += StartPage_Loaded;
        }

        private async void StartPage_Loaded(object sender, RoutedEventArgs e)
        {
            await _vm.Initialize();
            await Task.Run(StaticMethod.CheckForDatabase);

            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(TitleGrid);
            _vm.NavigateToFunction(MainFrame, MenuFunc.Uwp);
            //BottomListView.SelectedIndex = 0;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void BottomFunctionsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BottomListView.SelectedIndex != -1)
            {
                FunctionsListView.SelectedIndex = -1;
                MenuListItem m = BottomListView.SelectedItem as MenuListItem;
                Debug.Assert(m != null, "m != null");
                _vm.NavigateToFunction(MainFrame, m.MenuF);
                MainSplitView.IsPaneOpen = false;
            }
        }

        private void sideAd_AdMediatorError(object sender, Microsoft.AdMediator.Core.Events.AdMediatorFailedEventArgs e)
        {
            Debug.WriteLine("Ad error:" + e.Error);
        }

        private void PlatformButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (!(MainFrame.Content is UwpPage))
            {
                _vm.NavigateToFunction(MainFrame, MenuFunc.Uwp);
            }
        }
    }
}
