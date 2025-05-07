namespace ZhooSoft.Auth
{
    public interface IMainAppNavigation
    {
        void NavigateToMain(bool IsInitialLoad = false);

        void NavigateToDetail(NavigationPage detailPage);

        void NavigateToNotification();

        Task Initialize();
    }
}
