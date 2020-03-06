using MvvmCross.Converters;
using System;
using System.Globalization;

namespace Sliders.Core.Converters
{
    public class InvertedBoolConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}