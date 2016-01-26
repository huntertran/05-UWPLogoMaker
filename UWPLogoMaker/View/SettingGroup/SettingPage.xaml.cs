using System.Diagnostics;
using Windows.UI.Xaml.Controls;

namespace UWPLogoMaker.View.SettingGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pivot p = sender as Pivot;
            Debug.Assert(p != null, "p != null");
            switch (p.SelectedIndex)
            {
                case -1:
                    break;
                case 0:
                    //Save location
                    SettingFrame.Navigate(typeof (SaveLocationSettingPage));
                    break;
                case 1:
                    //About
                    SettingFrame.Navigate(typeof(AboutPage));
                    break;
                case 2:
                    //Rate and feedback
                    SettingFrame.Navigate(typeof (RateAndFeedbackPage));
                    break;
                case 3:
                    //Update database
                    SettingFrame.Navigate(typeof (UpdateDatabasePage));
                    break;
            }
        }
    }
}
