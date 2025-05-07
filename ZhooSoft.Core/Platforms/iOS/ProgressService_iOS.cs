using BigTed;
using UIKit;

namespace ZhooSoft.Core
{
    public class ProgressService_iOS : IProgressService
    {
        public void ShowProgress(string message)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                BTProgressHUD.Show(message, -1, MaskType.Black);
            });
        }

        public void HideProgress()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                BTProgressHUD.Dismiss();
            });
        }

        public void ShowToast(string message)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                BTProgressHUD.ShowToast(message, ToastPosition.Center, 2000);
            });
        }

        public void ShowAlertWithTextField(string title, string message, Action<bool, string> selection)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

                alertController.AddTextField(textField => textField.Placeholder = "Enter text");

                var cancelAction = UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, action => selection(false, "Cancel"));
                var okAction = UIAlertAction.Create("OK", UIAlertActionStyle.Default, action =>
                {
                    selection(true, alertController.TextFields[0].Text);
                });

                alertController.AddAction(cancelAction);
                alertController.AddAction(okAction);

                var window = UIApplication.SharedApplication.KeyWindow;
                var viewController = window?.RootViewController;
                while (viewController?.PresentedViewController != null)
                    viewController = viewController.PresentedViewController;

                viewController?.PresentViewController(alertController, true, null);
            });
        }
    }

}
