using CommunityToolkit.Maui.Views;
using ZCarsDriver.DPopupVM;

namespace ZCarsDriver.DPopup;

public partial class OnStartOtpPopup : Popup
{
	public OnStartOtpPopup()
	{
		InitializeComponent();
        BindingContext = new OnStartOtpViewModel();
        if (BindingContext is OnStartOtpViewModel vm)
        {
            vm.CurrentPopup = this;
        }
    }

    private void Otp_Completed(object sender, EventArgs e)
    {
        if (sender is Entry currentEntry)
        {
            // Move focus to next entry if not the last
            if (currentEntry == Otp1) Otp2.Focus();
            else if (currentEntry == Otp2) Otp3.Focus();
            else if (currentEntry == Otp3) Otp4.Focus();
        }
    }

    private void OnOtpTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry currentEntry && e.NewTextValue.Length == 1)
        {
            // Allow only numeric input (no negative or non-digit values)
            if (!int.TryParse(e.NewTextValue, out int number) || number < 0)
            {
                currentEntry.Text = string.Empty;
                return;
            }

            // Focus to next Entry if available
            if (currentEntry == Otp1) Otp2.Focus();
            else if (currentEntry == Otp2) Otp3.Focus();
            else if (currentEntry == Otp3) Otp4.Focus();
        }
    }

    private void HandleBackspace(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry currentEntry && e.NewTextValue.Length == 0)
        {
            if (currentEntry == Otp2) Otp1.Focus();
            else if (currentEntry == Otp3) Otp2.Focus();
            else if (currentEntry == Otp4) Otp3.Focus();
        }
    }
}