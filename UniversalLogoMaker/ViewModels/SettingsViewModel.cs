namespace UniversalLogoMaker.ViewModels
{
    using System;
    using System.Windows.Input;
    using Windows.ApplicationModel;
    using Windows.UI.Xaml;
    using Helpers;
    using Services;

    public class SettingsViewModel : Observable
    {
        public Visibility FeedbackLinkVisibility =>
            Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.IsSupported()
                ? Visibility.Visible
                : Visibility.Collapsed;

        private ICommand _launchFeedbackHubCommand;

        public ICommand LaunchFeedbackHubCommand
        {
            get
            {
                return _launchFeedbackHubCommand ?? (_launchFeedbackHubCommand = new RelayCommand(
                           async () =>
                           {
                               // This launcher is part of the Store Services SDK https://docs.microsoft.com/en-us/windows/uwp/monetize/microsoft-store-services-sdk
                               var launcher =
                                   Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
                               await launcher.LaunchAsync();
                           }));
            }
        }

        // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/pages/settings.md
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;

        public ElementTheme ElementTheme
        {
            get => _elementTheme;

            set => Set(ref _elementTheme, value);
        }

        private string _versionDescription;

        public string VersionDescription
        {
            get => _versionDescription;

            set => Set(ref _versionDescription, value);
        }

        private ICommand _switchThemeCommand;

        public ICommand SwitchThemeCommand
        {
            get
            {
                return _switchThemeCommand ?? (_switchThemeCommand = new RelayCommand<ElementTheme>(
                           async (param) =>
                           {
                               ElementTheme = param;
                               await ThemeSelectorService.SetThemeAsync(param);
                           }));
            }
        }

        public void Initialize()
        {
            VersionDescription = GetVersionDescription();
        }

        private static string GetVersionDescription()
        {
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{package.DisplayName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
