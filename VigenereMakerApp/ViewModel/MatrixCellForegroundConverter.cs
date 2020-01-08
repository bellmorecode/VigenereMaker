using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace VigenereMakerApp.ViewModel
{
    public class MatrixCellForegroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int row = System.Convert.ToInt32(values[0]);
            int col = System.Convert.ToInt32(values[1]);

            int sel_row = System.Convert.ToInt32(values[2]);
            int sel_col = System.Convert.ToInt32(values[3]);

            if (row == sel_row && col != sel_col) 
               return System.Windows.Media.Brushes.Blue;
            if (row != sel_row && col == sel_col) 
                return System.Windows.Media.Brushes.Blue;
            if (row == sel_row && col == sel_col) 
                return System.Windows.Media.Brushes.Green;

            return System.Windows.Media.Brushes.Black;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}