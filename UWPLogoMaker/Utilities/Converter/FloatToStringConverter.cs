using System;
using Windows.UI.Xaml.Data;

namespace UWPLogoMaker.Utilities.Converter
{
    public class FloatToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter == null)
            {
                return ((float) value).ToString("0.000");
            }
            else
            {
                return ((float)value).ToString("0");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return float.Parse(value.ToString());
        }
    }
}
