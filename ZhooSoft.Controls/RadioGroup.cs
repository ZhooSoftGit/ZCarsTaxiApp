using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhooSoft.Controls
{
    public class RadioGroup : StackLayout
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<string>), typeof(RadioGroup),
                null, propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty SelectedValueProperty =
            BindableProperty.Create(nameof(SelectedValue), typeof(string), typeof(RadioGroup),
                null, BindingMode.TwoWay);

        public static readonly BindableProperty GroupNameProperty =
            BindableProperty.Create(nameof(GroupName), typeof(string), typeof(RadioGroup), string.Empty);

        public IEnumerable<string> ItemsSource
        {
            get => (IEnumerable<string>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public string SelectedValue
        {
            get => (string)GetValue(SelectedValueProperty);
            set => SetValue(SelectedValueProperty, value);
        }

        public string GroupName
        {
            get => (string)GetValue(GroupNameProperty);
            set => SetValue(GroupNameProperty, value);
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is RadioGroup radioGroup && newValue is IEnumerable<string> items)
            {
                radioGroup.BuildRadioButtons();
            }
        }

        private void BuildRadioButtons()
        {
            Children.Clear();

            if (!string.IsNullOrEmpty(GroupName))
            {
                Children.Add(new Label { Text = GroupName, FontAttributes = FontAttributes.Bold });
            }

            foreach (var item in ItemsSource ?? Enumerable.Empty<string>())
            {
                var radioButton = new RadioButton
                {
                    Content = item,
                    GroupName = GroupName
                };

                radioButton.CheckedChanged += (s, e) =>
                {
                    if (e.Value)
                    {
                        SelectedValue = (string)radioButton.Content;
                    }
                };

                Children.Add(radioButton);
            }
        }
    }
}
