using System.Globalization;

namespace ZCarsDriver.Converters
{
    public class BoolToCheckImageConverter : IValueConverter
    {
        #region Methods

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is bool result && result == true ? "location.png" : "location.png"; // Replace with actual image paths
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
