namespace UWPLogoMaker.View.SettingGroup
{
    using Windows.Globalization;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using StartGroup;

    public sealed partial class LanguagePage
    {
        public LanguagePage()
        {
            InitializeComponent();
            Loaded += LanguagePage_Loaded;
        }

        private void LanguagePage_Loaded(object sender, RoutedEventArgs e)
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
            rootFrame?.Navigate(typeof(StartPage));
        }
    }
}