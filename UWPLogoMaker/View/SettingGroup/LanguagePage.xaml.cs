using Windows.Globalization;
using Windows.UI.Xaml.Controls;

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
    }
}
