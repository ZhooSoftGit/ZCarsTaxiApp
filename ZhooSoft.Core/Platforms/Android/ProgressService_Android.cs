using Android.App;
using Android.Widget;
using AndroidHUD;

namespace ZhooSoft.Core
{
    public class ProgressService_Android : IProgressService
    {
        public void ShowProgress(string message)
        {
            Platform.CurrentActivity?.RunOnUiThread(() =>
            {
                AndHUD.Shared.Show(Platform.CurrentActivity, message, -1, MaskType.Black);
            });
        }

        public void HideProgress()
        {
            Platform.CurrentActivity?.RunOnUiThread(() =>
            {
                AndHUD.Shared.Dismiss();
            });
        }

        public void ShowToast(string message)
        {
            Platform.CurrentActivity?.RunOnUiThread(() =>
            {
                Toast.MakeText(Platform.CurrentActivity, message, ToastLength.Short)?.Show();
            });
        }

        public void ShowAlertWithTextField(string title, string message, Action<bool, string> selection)
        {
            Platform.CurrentActivity?.RunOnUiThread(() =>
            {
                var alert = new AlertDialog.Builder(Platform.CurrentActivity);
                alert.SetTitle(title);
                alert.SetMessage(message);

                var editText = new EditText(Platform.CurrentActivity)
                {
                    Hint = "Enter text here"
                };
                alert.SetView(editText);

                alert.SetPositiveButton("OK", (sender, args) =>
                {
                    selection(true, editText.Text);
                });

                alert.SetNegativeButton("Cancel", (sender, args) =>
                {
                    selection(false, "Cancel");
                });

                alert.Show();
            });
        }
    }
}

