using System;
using System.Globalization;
using System.Windows.Data;

namespace VigenereMakerApp.ViewModel
{
    public class MatrixLegendLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var term = (string)value;
            var offset = System.Convert.ToInt32((string)parameter);
            if (offset >= term.Length) return "?";
            return string.Format("{0}", term[offset]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}