namespace ZhooSoft.Core
{
    public class HelperService : IHelperService
    {
        public async Task ShowAlert(string title, string message, string cancel)
        {
            if (Application.Current != null && Application.Current.Windows != null && Application.Current.Windows.Count > 0)
            {
                if (Application.Current.Windows[0].Page is Page page)
                {
                    await page.DisplayAlert(title, message, cancel);
                }
            }
        }

        public async Task<bool> ShowConfirmation(string title, string message, string accept, string cancel)
        {
            if (Application.Current != null && Application.Current.Windows != null && Application.Current.Windows.Count > 0)
            {
                if (Application.Current.Windows[0].Page is Page page)
                {
                    return await page.DisplayAlert(title, message, accept, cancel);
                }
            }
            return false;
        }
    }
}
