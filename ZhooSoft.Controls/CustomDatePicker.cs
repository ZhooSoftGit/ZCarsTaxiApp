using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZhooSoft.Controls
{
    public class CustomDatePicker : DatePicker
    {
        public static readonly BindableProperty DateSelectedCommandProperty =
            BindableProperty.Create(nameof(DateSelectedCommand), typeof(ICommand), typeof(CustomDatePicker));

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomDatePicker));

        public ICommand DateSelectedCommand
        {
            get => (ICommand)GetValue(DateSelectedCommandProperty);
            set => SetValue(DateSelectedCommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public CustomDatePicker()
        {
            DateSelected += CustomDatePicker_DateSelected;
        }

        private void CustomDatePicker_DateSelected(object? sender, DateChangedEventArgs e)
        {
            DateSelectedCommand?.Execute(this.Date);
        }
    }
}
