using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTaxiApp.Converters
{
    public class ChatAlignmentConverter : IValueConverter
    {
        // Converts IsIncoming bool → Horizontal alignment
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isIncoming)
            {
                return isIncoming ? LayoutOptions.Start : LayoutOptions.End;
            }

            // Fallback alignment
            return LayoutOptions.Start;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
