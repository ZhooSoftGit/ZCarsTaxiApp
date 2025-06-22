using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZhooSoft.Core;

namespace ZTaxiApp.ViewModel
{
    public partial class ProfileViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _name = "Rajesh";

        [ObservableProperty]
        private string _phoneNumber = "+91 8344273152";

        [ObservableProperty]
        private string _email = "rajesh13it@gmail.com";

        [ObservableProperty]
        private string _gender = "Male";

        [ObservableProperty]
        private DateTime? _dateOfBirth = null;

        public string DateOfBirthDisplay => DateOfBirth?.ToString("dd MMM yyyy") ?? "Required";
        public Color DateOfBirthColor => DateOfBirth is null ? Colors.OrangeRed : Colors.Gray;

        [ObservableProperty]
        private string _memberSinceDisplay = "October 2023";

        [ObservableProperty]
        private string _emergencyContact = null;

        public string EmergencyContactDisplay => EmergencyContact ?? "Required";
        public Color EmergencyContactColor => EmergencyContact is null ? Colors.OrangeRed : Colors.Gray;

        public IRelayCommand<string> EditCommand { get; }

        public ProfileViewModel()
        {
            EditCommand = new RelayCommand<string>(OnEditRequested);
        }

        private async void OnEditRequested(string parameter)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Edit",
                $"Navigate to edit {parameter}.",
                "OK");
        }

        // Ensure the UI updates for derived properties
        partial void OnDateOfBirthChanged(DateTime? value)
        {
            OnPropertyChanged(nameof(DateOfBirthDisplay));
            OnPropertyChanged(nameof(DateOfBirthColor));
        }

        partial void OnEmergencyContactChanged(string value)
        {
            OnPropertyChanged(nameof(EmergencyContactDisplay));
            OnPropertyChanged(nameof(EmergencyContactColor));
        }
    }
}
