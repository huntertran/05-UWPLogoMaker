using System;
using Windows.UI.Xaml.Data;

namespace UWPLogoMaker.Utilities.Converter
{
    public class NullToEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
