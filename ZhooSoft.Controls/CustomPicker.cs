using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZhooSoft.Controls
{
    public class CustomPicker : Picker
    {

        public static readonly BindableProperty PickerSelectedCommandProperty =
            BindableProperty.Create(nameof(PickerSelectedCommand), typeof(ICommand), typeof(CustomPicker));

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomPicker));

        public ICommand PickerSelectedCommand
        {
            get => (ICommand)GetValue(PickerSelectedCommandProperty);
            set => SetValue(PickerSelectedCommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public CustomPicker()
        {
            SelectedIndexChanged += CustomPicker_SelectedIndexChanged;
        }

        private void CustomPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            PickerSelectedCommand?.Execute(SelectedItem);
        }
    }
}
