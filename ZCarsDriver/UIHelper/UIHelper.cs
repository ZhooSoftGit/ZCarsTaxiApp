using ZhooCars.Common;
using ZhooCars.Model.DTOs;

namespace ZCarsDriver.UIHelper
{
    public static class UIHelper
    {
        #region Methods

        public static double GetFileSizeAsync(FileResult fileResult)
        {
            var filePath = fileResult.FullPath; // Works on Windows, MacCatalyst, and Android (not iOS)
            var fileInfo = new FileInfo(filePath);
            var kbsize = fileInfo.Length / 1024.0;
            return kbsize; // File size in bytes
        }

        public static async Task<ImageSource?> GetImageSource(FileResult? result = null)
        {
            if (result == null)
                result = await FilePicker.Default.PickAsync();

            if (result != null && (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase)))
            {
                Stream? imageStream = await result.OpenReadAsync().ConfigureAwait(true);
                if (imageStream is not null)
                {
                    // Create a copy of the stream to avoid disposing issues
                    MemoryStream copyStream = new();
                    await imageStream.CopyToAsync(copyStream).ConfigureAwait(true);
                    copyStream.Position = 0; // Reset position to the beginning

                    // Use the copied stream to create the ImageSource
                    return ImageSource.FromStream(() => new MemoryStream(copyStream.ToArray())); // <=== Include a new MemoryStream
                }
            }

            return null;
        }

        public static async Task<FileResult> PickFile()
        {
            var result = await FilePicker.Default.PickAsync();
            return result;
        }

        public static async Task<FileResult> TakePhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    return photo;
                }
            }
            return null;
        }

        #endregion
    }

    public static class VehicleDtoSamples
    {
        public static List<VehicleDto> GetSampleVehicles()
        {
            return new List<VehicleDto>
            {
                new VehicleDto
                {
                    ApprovalStatus = ApprovalStatus.Approved,
                    Color = "Blue",
                    FuelType = "Diesel",
                    InsuranceExpiryDate = DateTime.Now.AddYears(1),
                    InsuranceNumber = "INS12345",
                    RCNumber = "RC123",
                    RegistrationDate = DateTime.Now.AddYears(-3),
                    SeatingCapacity = 5,
                    Status = VehicleStatus.Active,
                    VehicleId = 1,
                    VehicleMake = "Toyota",
                    VehicleModel = "Innova",
                    VehicleRegistrationNumber = "KA01AB1234",
                    VehicleTypeId = 2,
                    VehicleYear = 2020
                },
                new VehicleDto
                {
                    ApprovalStatus = ApprovalStatus.Pending,
                    Color = "Red",
                    FuelType = "Petrol",
                    InsuranceExpiryDate = DateTime.Now.AddMonths(6),
                    InsuranceNumber = "INS67890",
                    RCNumber = "RC456",
                    RegistrationDate = DateTime.Now.AddYears(-1),
                    SeatingCapacity = 4,
                    Status = VehicleStatus.Maintenance,
                    VehicleId = 2,
                    VehicleMake = "Honda",
                    VehicleModel = "City",
                    VehicleRegistrationNumber = "MH02CD5678",
                    VehicleTypeId = 1,
                    VehicleYear = 2022
                },
                new VehicleDto
                {
                    ApprovalStatus = ApprovalStatus.Rejected,
                    Color = "White",
                    FuelType = "Electric",
                    InsuranceExpiryDate = DateTime.Now.AddYears(2),
                    InsuranceNumber = "INS11223",
                    RCNumber = "RC789",
                    RegistrationDate = DateTime.Now.AddYears(-5),
                    SeatingCapacity = 7,
                    Status = VehicleStatus.Active,
                    VehicleId = 3,
                    VehicleMake = "Tesla",
                    VehicleModel = "Model X",
                    VehicleRegistrationNumber = "DL03EF9012",
                    VehicleTypeId = 3,
                    VehicleYear = 2021
                }
            };
        }
    }

}
