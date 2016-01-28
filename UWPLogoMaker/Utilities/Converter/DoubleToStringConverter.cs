using System;
using Windows.UI.Xaml.Data;

namespace UWPLogoMaker.Utilities.Converter
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return double.Parse(value.ToString());
        }
    }
}
