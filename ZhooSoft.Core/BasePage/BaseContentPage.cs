namespace ZhooSoft.Core
{
    public abstract class BaseContentPage<T> : ContentPage, IViewFor<T> where T : ViewModelBase, new()
    {
        private T _viewModel;

        public T ViewModel
        {
            get
            {
                return _viewModel;
            }

            set
            {
                _viewModel = value;

                BindingContext = _viewModel;
            }
        }

        object IViewFor.ViewModel
        {
            get { return _viewModel; }
            set
            {
                ViewModel = (T)value;
            }
        }

        protected BaseContentPage()
        {
            ViewModel = ServiceHelper.GetService<T>();
        }

        protected override void OnAppearing()
        {
            _viewModel?.OnAppearing();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            _viewModel?.OnDisappearing();
            base.OnDisappearing();
        }
    }
}
