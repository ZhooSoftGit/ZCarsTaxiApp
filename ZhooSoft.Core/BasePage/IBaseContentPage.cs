namespace ZhooSoft.Core
{
    public interface IViewFor
    {
        object ViewModel { get; set; }
    }

    public interface IViewFor<T> : IViewFor where T : ViewModelBase, new()
    {
        new T ViewModel { get; set; }
    }
}
