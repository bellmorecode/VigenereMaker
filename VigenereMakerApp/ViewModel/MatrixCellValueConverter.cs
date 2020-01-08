using System;
using System.Globalization;
using System.Windows.Data;

namespace VigenereMakerApp.ViewModel
{
    public class MatrixCellValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string[] arr = (string[])values[0];
            if (arr == null) return string.Empty;
            int row = System.Convert.ToInt32(values[1]);
            int col = System.Convert.ToInt32(values[2]);

            return string.Format("{0}", arr[row][col]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}