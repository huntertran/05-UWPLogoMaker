using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using UWPLogoMaker.View.StartGroup;

namespace UWPLogoMaker.View.SettingGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LanguagePage
    {
        public LanguagePage()
        {
            InitializeComponent();
            Loaded += LanguagePage_Loaded;
        }

        private void LanguagePage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //ApplicationLanguages.
        }

        private void English_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            UpdateLanguage("en-US");
        }


        private void Vietnamese_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            UpdateLanguage("vi-VN");
        }

        private void UpdateLanguage(string language)
        {
            ApplicationLanguages.PrimaryLanguageOverride = language;
            //Frame.Navigate(this.GetType());
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(StartPage));
        }
    }
}
