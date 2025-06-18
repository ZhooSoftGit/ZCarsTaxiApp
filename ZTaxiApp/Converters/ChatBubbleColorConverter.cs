using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTaxiApp.Converters
{
    public class ChatBubbleColorConverter : IValueConverter
    {
        // Converts IsIncoming bool → Color
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isIncoming)
            {
                return isIncoming
                    ? Color.FromArgb("#E0E0E0")  // Light Gray for incoming
                    : Color.FromArgb("#FFF176"); // Yellow for outgoing
            }

            // Fallback color
            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
