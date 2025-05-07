using Microsoft.Maui.Controls.Shapes;

namespace ZhooSoft.Controls
{
    public class CustomBorder : Border
    {

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(CustomBorder), 10f);

        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomBorder), Colors.Black);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public CustomBorder()
        {
            StrokeThickness = 2;
            CornerRadius = 10;
            StrokeShape = new RoundRectangle { CornerRadius = CornerRadius };
            Stroke = BorderColor;
        }
    }

}
