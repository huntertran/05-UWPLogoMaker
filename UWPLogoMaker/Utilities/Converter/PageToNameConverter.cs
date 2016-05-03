using System;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml.Data;
using UWPLogoMaker.View.StartGroup;

namespace UWPLogoMaker.Utilities.Converter
{
    public class PageToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string defaultName =
                ResourceManager.Current.MainResourceMap.GetValue(
                    "Resources/PageToNameConverter_Convert_Windows_Universal_App_Logo_Maker", new ResourceContext())
                    .ValueAsString;

            if (value is StartPage)
            {
                return defaultName;
            }

            return defaultName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}