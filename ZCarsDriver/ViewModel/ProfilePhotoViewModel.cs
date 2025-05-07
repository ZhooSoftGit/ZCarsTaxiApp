using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class ProfilePhotoViewModel : ViewModelBase
    {
        #region Fields

        [ObservableProperty]
        private ImageSource profilePhoto;

        #endregion

        #region Constructors

        public ProfilePhotoViewModel()
        {
            UploadPhotoCommand = new Command(async () => await UploadPhotoAsync());
            TakePhotoCommand = new Command(async () => await TakePhotoAsync());
            RemovePhotoCommand = new Command(RemovePhoto);
            SaveCommand = new Command(SaveProfilePhoto);
        }

        #endregion

        #region Properties

        public ICommand RemovePhotoCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand TakePhotoCommand { get; }

        public ICommand UploadPhotoCommand { get; }

        #endregion

        #region Methods

        private void RemovePhoto()
        {
            ProfilePhoto = null;
        }

        private void SaveProfilePhoto()
        {
            if (ProfilePhoto == null)
            {
                Console.WriteLine("Please upload or capture a photo before saving.");
                return;
            }

            Console.WriteLine("Profile photo saved successfully.");
        }

        private async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null)
                {
                    using var stream = await photo.OpenReadAsync();
                    ProfilePhoto = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error capturing photo: {ex.Message}");
            }
        }

        private async Task UploadPhotoAsync()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    using var stream = await result.OpenReadAsync();
                    ProfilePhoto = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error picking photo: {ex.Message}");
            }
        }

        #endregion
    }
}
