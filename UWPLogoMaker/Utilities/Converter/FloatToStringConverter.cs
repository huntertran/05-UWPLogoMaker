using System;
using Windows.UI.Xaml.Data;

namespace UWPLogoMaker.Utilities.Converter
{
    public class FloatToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((float) value).ToString("0.000");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return float.Parse(value.ToString());
        }
    }
}
