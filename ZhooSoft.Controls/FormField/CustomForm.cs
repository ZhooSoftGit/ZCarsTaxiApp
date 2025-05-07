using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ZhooSoft.Controls
{
    public class FormControl : VerticalStackLayout
    {
        public static readonly BindableProperty FormFieldsProperty =
            BindableProperty.Create(nameof(FormFields), typeof(ObservableCollection<FormField>), typeof(FormControl), new ObservableCollection<FormField>(), propertyChanged: OnFormFieldsChanged);

        public ICommand FormValueChangedCommand { get; }

        public FormControl()
        {
            FormValueChangedCommand = new Command(() => OnFormValueChanged());
        }
        private void OnFormValueChanged()
        {
            FormValueChangedCommand.Execute(null);
        }
        public ObservableCollection<FormField> FormFields
        {
            get => (ObservableCollection<FormField>)GetValue(FormFieldsProperty);
            set => SetValue(FormFieldsProperty, value);
        }

        private static void OnFormFieldsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is FormControl control && newValue is ObservableCollection<FormField> fields)
            {
                control.GenerateForm(fields);
            }
        }

        private void GenerateForm(ObservableCollection<FormField> fields)
        {
            this.Children.Clear(); // Clear previous UI elements

            foreach (var field in fields)
            {
                Label label = new Label
                {
                    Text = field.Label,
                    FontAttributes = FontAttributes.Bold
                };

                View inputControl = null;

                switch (field.Type)
                {
                    case FieldType.Text:
                        inputControl = new SfEntry
                        {
                            Hint = field.Placeholder,
                            SfText = field.Value,
                            BindingContext = field
                        };
                        inputControl.SetBinding(SfEntry.SfTextProperty, nameof(FormField.Value), BindingMode.TwoWay);
                        break;

                    case FieldType.Number:
                        inputControl = new SfEntry
                        {
                            Keyboard = Keyboard.Numeric,
                            Hint = field.Placeholder,
                            SfText = field.Value,
                            BindingContext = field
                        };
                        inputControl.SetBinding(SfEntry.SfTextProperty, nameof(FormField.Value), BindingMode.TwoWay);
                        break;

                    case FieldType.Telephone:
                        inputControl = new SfEntry
                        {
                            Keyboard = Keyboard.Telephone,
                            Hint = field.Placeholder,
                            SfText = field.Value,
                            BindingContext = field
                        };
                        inputControl.SetBinding(SfEntry.SfTextProperty, nameof(FormField.Value), BindingMode.TwoWay);
                        break;

                    case FieldType.Email:
                        inputControl = new SfEntry
                        {
                            Keyboard = Keyboard.Email,
                            Hint = field.Placeholder,
                            SfText = field.Value,
                            BindingContext = field
                        };
                        inputControl.SetBinding(SfEntry.SfTextProperty, nameof(FormField.Value), BindingMode.TwoWay);
                        break;

                    case FieldType.Date:
                        inputControl = new DatePicker
                        {
                            BindingContext = field
                        };
                        inputControl.SetBinding(DatePicker.DateProperty, nameof(FormField.DateValue), BindingMode.TwoWay);
                        break;

                    case FieldType.Picker:
                        var picker = new Picker
                        {
                            ItemsSource = field.Options,
                            BindingContext = field
                        };
                        if (!string.IsNullOrEmpty(field.PickerDisplayProperty))
                        {
                            picker.ItemDisplayBinding = new Binding(field.PickerDisplayProperty);
                        }

                        picker.SetBinding(Picker.SelectedItemProperty, nameof(FormField.Value), BindingMode.TwoWay);
                        inputControl = picker;
                        break;

                    case FieldType.Checkbox:
                        var checkbox = new CheckBox
                        {
                            BindingContext = field
                        };
                        checkbox.SetBinding(CheckBox.IsCheckedProperty, nameof(FormField.IsChecked), BindingMode.TwoWay);
                        inputControl = checkbox;
                        break;
                    case FieldType.RadioButton:
                        var rg = new RadioGroup
                        {
                            BindingContext = field
                        };
                        rg.ItemsSource = field.Options.Select(o => o.ToString()).ToList();
                        rg.Orientation = field.StackOrientation;
                        rg.SetBinding(RadioGroup.SelectedValueProperty, nameof(FormField.Value), BindingMode.TwoWay);
                        inputControl = rg;
                        break;
                }

                this.Children.Add(label);
                if (inputControl != null)
                    this.Children.Add(inputControl);
            }
        }
    }

    public enum FieldType
    {
        Text,
        Number,
        Telephone,
        Email,
        Date,
        Picker,
        Checkbox,
        RadioButton
    }

    public partial class FormField : ObservableObject
    {
        [ObservableProperty]
        private string _label;

        [ObservableProperty]
        private FieldType _type;

        [ObservableProperty]
        private string? _value;

        [ObservableProperty]
        private bool? _isChecked;

        [ObservableProperty]
        private bool _isRequired;

        [ObservableProperty]
        private List<object> _options;

        [ObservableProperty]
        private object _selectedItem;

        [ObservableProperty]
        private string _pickerDisplayProperty;

        [ObservableProperty]
        private string _placeholder;

        [ObservableProperty]
        private DateTime? _dateValue;

        public StackOrientation StackOrientation = StackOrientation.Horizontal;
    }
}
