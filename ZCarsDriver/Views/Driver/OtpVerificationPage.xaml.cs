using ZCarsDriver.ViewModel;
using ZhooSoft.Controls;
using ZhooSoft.Core;

namespace ZCarsDriver.Views.Driver;

public partial class OtpVerificationPage : BaseContentPage<OtpVerificationViewModel>
{
    private List<Entry> _otpEntries;
    public OtpVerificationPage()
	{
		InitializeComponent();
        // Store the OTP entry fields in a list for easier navigation
        _otpEntries = new List<Entry> { otp1, otp2, otp3, otp4 };
    }

    private void OtpEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry)
        {
            int index = _otpEntries.IndexOf(entry);

            // Move focus to next entry when a digit is entered
            if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length == 1)
            {
                if (index < _otpEntries.Count - 1)
                {
                    _otpEntries[index + 1].Focus();
                }
            }
        }
    }

    private void OtpEntry_Unfocused(object sender, FocusEventArgs e)
    {
        if (sender is CustomEntry entry)
        {
            entry.TextChanged -= OtpEntry_Backspace;
            entry.TextChanged += OtpEntry_Backspace;
        }
    }

    private void OtpEntry_Backspace(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry && e.NewTextValue == string.Empty)
        {
            int index = _otpEntries.IndexOf(entry);

            // Move focus to previous entry when backspace is pressed
            if (index > 0)
            {
                _otpEntries[index - 1].Focus();
                _otpEntries[index - 1].Text = ""; // Clear previous entry
            }
        }
    }
}