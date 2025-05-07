using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZhooSoft.Controls
{
    public class CustomImage : Image
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CustomDatePicker));

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomDatePicker));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public CustomImage()
        {
            AddGesture();
        }

        private void AddGesture()
        {
            var tapgesture = new TapGestureRecognizer();
            tapgesture.Tapped += Tapgesture_Tapped;
            GestureRecognizers.Add(tapgesture);
        }

        private void Tapgesture_Tapped(object? sender, TappedEventArgs e)
        {
            Command?.Execute(CommandParameter);
        }
    }
}
