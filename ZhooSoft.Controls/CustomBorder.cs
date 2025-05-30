using Microsoft.Maui.Controls.Shapes;
using System.Runtime.CompilerServices;
using System.Windows.Input;

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
            var tapgesture = new TapGestureRecognizer();
            GestureRecognizers.Add(tapgesture);
            tapgesture.Tapped -= Tapgesture_Tapped;
            tapgesture.Tapped += Tapgesture_Tapped;
        }

        #region Properties

        public ICommand ClickCommand { get => (ICommand)GetValue(ClickCommandProperty); set => SetValue(ClickCommandProperty, value); }


        public static BindableProperty ClickCommandProperty =
           BindableProperty.Create(nameof(ClickCommand), typeof(ICommand), typeof(CustomBorder));
        #endregion

        #region Methods

        private void Tapgesture_Tapped(object sender, System.EventArgs e)
        {
            ClickCommand?.Execute(e);
        }

        protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(BorderColor))
            {
                Stroke = BorderColor;
            }

            if (propertyName == nameof(CornerRadius))
            {
                StrokeShape = new RoundRectangle() { CornerRadius = CornerRadius };
            }
        }

        #endregion
    }

}
