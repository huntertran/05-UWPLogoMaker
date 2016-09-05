using System;
using Windows.UI.Xaml.Data;

namespace UWPLogoMaker.Utilities.Converter
{
    public class ReversedBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool) value)
            {
                return !(bool) value;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
            {
                return !(bool)value;
            }
            return false;
        }
    }
}
