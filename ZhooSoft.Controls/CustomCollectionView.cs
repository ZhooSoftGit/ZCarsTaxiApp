namespace ZhooSoft.Controls
{
    public class CustomCollectionView : CollectionView
    {
        public static readonly BindableProperty DesiredItemWidthProperty =
            BindableProperty.Create(nameof(DesiredItemWidth), typeof(double), typeof(CustomCollectionView), 150.0);

        public static readonly BindableProperty ItemHeightProperty =
            BindableProperty.Create(nameof(ItemHeight), typeof(double), typeof(CustomCollectionView), 100.0);

        public double DesiredItemWidth
        {
            get => (double)GetValue(DesiredItemWidthProperty);
            set => SetValue(DesiredItemWidthProperty, value);
        }

        public double ItemHeight
        {
            get => (double)GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        public CustomCollectionView()
        {
            ItemsLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Vertical);
            SizeChanged += OnSizeChanged;
            PropertyChanged += OnPropertyChangedHandler;
        }

        private void OnPropertyChangedHandler(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ItemsSource))
            {
                UpdateHeight();
            }
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            AdjustSpan();
        }

        private void AdjustSpan()
        {
            if (ItemsLayout is GridItemsLayout gridLayout && DesiredItemWidth > 0 && Width > 0)
            {
                int span = Math.Max(1, (int)(GetScreenWidth() / DesiredItemWidth));
                gridLayout.Span = span;
                UpdateHeight();
            }
        }

        private double GetScreenWidth()
        {
            double widthInPixels = DeviceDisplay.MainDisplayInfo.Width;
            double density = DeviceDisplay.MainDisplayInfo.Density;
            double widthInDp = widthInPixels / density;

            return widthInDp;
        }

        private void UpdateHeight()
        {
            if (ItemsLayout is GridItemsLayout gridLayout)
            {
                int count = Getcount();
                int span = gridLayout.Span;
                int rows = (int)Math.Ceiling((double)count / span);

                double totalHeight = rows * ItemHeight;
                HeightRequest = totalHeight;
            }
        }

        int Getcount()
        {
            if (ItemsSource != null)
                return ItemsSource.Cast<Object>().Count();
            else
                return 0;
        }

    }
}

