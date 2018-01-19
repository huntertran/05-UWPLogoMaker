﻿namespace UWPLogoMaker.Utilities.Converter
{
    using System;
    using Windows.ApplicationModel.Resources.Core;
    using Windows.UI.Xaml.Data;
    using View.NewSizeGroup;
    using View.SettingGroup;
    using View.StartGroup;

    public class PageToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var defaultName =
                ResourceManager.Current.MainResourceMap.GetValue(
                        "Resources/PageToNameConverter_Convert_Windows_Universal_App_Logo_Maker", new ResourceContext())
                    .ValueAsString;

            if (value is StartPage)
            {
                return defaultName;
            }

            if (value is SettingPage)
            {
                return
                    ResourceManager.Current.MainResourceMap.GetValue("Resources/PageToNameConverter_Convert_Setting",
                        new ResourceContext()).ValueAsString;
            }

            if (value is NewSizePage)
            {
                return
                    ResourceManager.Current.MainResourceMap.GetValue(
                        "Resources/PageToNameConverter_Convert_Add_new_size", new ResourceContext()).ValueAsString;
            }

            return defaultName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}