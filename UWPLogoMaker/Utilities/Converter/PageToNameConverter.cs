using System;
using Windows.UI.Xaml.Data;
using UWPLogoMaker.View.StartGroup;

namespace UWPLogoMaker.Utilities.Converter
{
    public class PageToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string defaultName = "Windows Universal App Logo Maker";

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
