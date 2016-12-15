using System;
using System.Linq;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using UWPLogoMaker.Model;
using UWPLogoMaker.ViewModel.SettingGroup;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPLogoMaker.View.SettingGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MoreAppPage : Page
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
