namespace UniversalLogoMaker.Utilities.Converter
{
    using System;
    using Windows.UI.Xaml.Data;

    public class FloatToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((float) value).ToString(parameter == null
                ? "0.000"
                : "0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return float.Parse(value.ToString());
        }
    }
}