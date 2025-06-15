namespace ZhooSoft.Controls
{
    public class CustomButton : Button
    {
        public CustomButton()
        {
            // Monitor IsEnabled changes to adjust opacity
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(IsEnabled))
                {
                    Opacity = IsEnabled ? 1 : 0.5;
                }
            };

            // Initialize correct opacity
            Opacity = IsEnabled ? 1 : 0.5;
        }
    }
}
