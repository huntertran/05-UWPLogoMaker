﻿namespace UniversalLogoMaker.Services
{
    using System;
    using System.Threading.Tasks;
    using Helpers;
    using Views;

    public static class FirstRunDisplayService
    {
        internal static async Task ShowIfAppropriateAsync()
        {
            bool hasShownFirstRun = false;
            hasShownFirstRun =
                await Windows.Storage.ApplicationData.Current.LocalSettings.ReadAsync<bool>(nameof(hasShownFirstRun));

            if (!hasShownFirstRun)
            {
                await Windows.Storage.ApplicationData.Current.LocalSettings.SaveAsync(nameof(hasShownFirstRun), true);
                var dialog = new FirstRunDialog();
                await dialog.ShowAsync();
            }
        }
    }
}
