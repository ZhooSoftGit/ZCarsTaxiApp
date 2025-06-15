using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZhooSoft.Controls
{
    public class CustomActionView : ContentView
    {
        public static readonly BindableProperty FirstActionTextProperty =
            BindableProperty.Create(nameof(FirstActionText), typeof(string), typeof(CustomActionView), default(string));

        public static readonly BindableProperty SecondActionTextProperty =
            BindableProperty.Create(nameof(SecondActionText), typeof(string), typeof(CustomActionView), default(string));

        public static readonly BindableProperty ThirdActionTextProperty =
            BindableProperty.Create(nameof(ThirdActionText), typeof(string), typeof(CustomActionView), default(string));

        public static readonly BindableProperty SelectedTextProperty =
            BindableProperty.Create(nameof(SelectedText), typeof(string), typeof(CustomActionView), default(string), propertyChanged: OnSelectedChanged);

        public static readonly BindableProperty SelectionChangedCommandProperty =
            BindableProperty.Create(nameof(SelectionChangedCommand), typeof(ICommand), typeof(CustomActionView), default(ICommand));

        public string FirstActionText
        {
            get => (string)GetValue(FirstActionTextProperty);
            set => SetValue(FirstActionTextProperty, value);
        }

        public string SecondActionText
        {
            get => (string)GetValue(SecondActionTextProperty);
            set => SetValue(SecondActionTextProperty, value);
        }

        public string ThirdActionText
        {
            get => (string)GetValue(ThirdActionTextProperty);
            set => SetValue(ThirdActionTextProperty, value);
        }

        public string SelectedText
        {
            get => (string)GetValue(SelectedTextProperty);
            set => SetValue(SelectedTextProperty, value);
        }

        public ICommand SelectionChangedCommand
        {
            get => (ICommand)GetValue(SelectionChangedCommandProperty);
            set => SetValue(SelectionChangedCommandProperty, value);
        }

        private Grid _layoutGrid;

        public CustomActionView()
        {
            BuildLayout();
        }

        private void BuildLayout()
        {
            _layoutGrid = new Grid
            {
                ColumnSpacing = 0,
                Padding = 2,
                BackgroundColor = Colors.Transparent
            };

            Content = new Border
            {
                BackgroundColor = Color.FromArgb("#E6FAF7"),
                Stroke = Color.FromArgb("#007D8A"),
                StrokeShape = new RoundRectangle { CornerRadius = 30 },
                StrokeThickness = 1.5f,
                Padding = 0,
                Content = _layoutGrid
            };

            RefreshSegments();
        }

        private void RefreshSegments()
        {
            _layoutGrid.Children.Clear();
            _layoutGrid.ColumnDefinitions.Clear();

            var actions = new List<string>();
            if (!string.IsNullOrWhiteSpace(FirstActionText)) actions.Add(FirstActionText);
            if (!string.IsNullOrWhiteSpace(SecondActionText)) actions.Add(SecondActionText);
            if (!string.IsNullOrWhiteSpace(ThirdActionText)) actions.Add(ThirdActionText);

            int count = actions.Count;

            for (int i = 0; i < count; i++)
            {
                _layoutGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

                var text = actions[i];
                var label = new Label
                {
                    Text = text,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = text == SelectedText ? Colors.White : Color.FromArgb("#007D8A"),
                    Margin = new Thickness(6)
                };

                var background = new Border
                {
                    BackgroundColor = text == SelectedText ? Color.FromArgb("#007D8A") : Colors.Transparent,
                    StrokeShape = new RoundRectangle { CornerRadius = 28 },
                    Content = label,
                    Padding = new Thickness(12, 6)
                };


                int column = i;
                Grid.SetColumn(background, column);
                _layoutGrid.Children.Add(background);

                var tap = new TapGestureRecognizer();
                tap.Tapped += (s, e) =>
                {
                    SelectedText = text;
                    SelectionChangedCommand?.Execute(text);
                };

                background.GestureRecognizers.Add(tap);
            }
        }

        private static void OnSelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomActionView view)
                view.RefreshSegments();
        }
    }
}