namespace UWPLogoMaker.Utilities.Converter
{
    using System;
    using Windows.UI.Xaml.Data;

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
            if ((bool) value)
            {
                return !(bool) value;
            }

            return false;
        }
    }
}