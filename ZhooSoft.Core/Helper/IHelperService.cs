namespace ZhooSoft.Core
{
    public interface IHelperService
    {
        Task ShowAlert(string title, string message, string cancel);
        Task<bool> ShowConfirmation(string title, string message, string accept, string cancel);
    }
}
