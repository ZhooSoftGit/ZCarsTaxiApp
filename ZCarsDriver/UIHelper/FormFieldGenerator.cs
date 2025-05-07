using ZhooSoft.Controls;

namespace ZCarsDriver.UIHelper
{
    public class FormFieldGenerator
    {
        #region Methods

        public static List<FormField> GenerateFormFields(CheckListType checkListType, object? data = null)
        {
            var formFields = new List<FormField>();

            switch (checkListType)
            {
                case CheckListType.BasicDetails:
                    formFields.AddRange(new List<FormField>
                {
                    new() { Label = "Full Name", Type = FieldType.Text, Placeholder = "Enter full name", IsRequired = true, Value = GetValue<string>(data, "FullName") },
                    new() { Label = "Gender", Type = FieldType.RadioButton, Options = new List<object> { "Male", "Female", "Other" }, IsRequired = true, Value = GetValue<string>(data, "Gender") },
                    new() { Label = "Date of Birth", Type = FieldType.Date, IsRequired = true, Value = GetValue<string>(data, "DateOfBirth") },
                    new() { Label = "Phone Number", Type = FieldType.Number, Placeholder = "Enter phone number", IsRequired = true, Value = GetValue<string>(data, "PhoneNumber") },
                    new() { Label = "Email Address", Type = FieldType.Text, Placeholder = "Enter email (optional)", Value = GetValue<string>(data, "EmailAddress") },
                    new() { Label = "City", Type = FieldType.Text, Placeholder = "Enter City", Value = GetValue<string>(data, "City") },
                    new() { Label = "PinCode", Type = FieldType.Number, Placeholder = "Enter PinCode", Value = GetValue<string>(data, "Pincode") }
                    });
                    break;

                case CheckListType.VehicleDetails:
                    formFields.AddRange(new List<FormField>
                {
                    new() { Label = "Vehicle Make/Brand", Type = FieldType.Text, Placeholder = "Enter vehicle brand", IsRequired = true, Value = GetValue<string>(data, "VehicleMake") },
                    new() { Label = "Vehicle Model", Type = FieldType.Text, Placeholder = "Enter vehicle model", IsRequired = true, Value = GetValue<string>(data, "VehicleModel") },
                    new() { Label = "Vehicle Year of Manufacture", Type = FieldType.Number, Placeholder = "Enter year (e.g., 2020)", IsRequired = true, Value = GetValue<string>(data, "VehicleYear") },
                    new() { Label = "Vehicle Color", Type = FieldType.Text, Placeholder = "Enter vehicle color", IsRequired = true, Value = GetValue<string>(data, "Color") },
                    new() { Label = "Vehicle Type", Type = FieldType.Picker, Options = new List<object> { "Hatchback", "Sedan", "SUV", "MPV", "Luxury", "Bike Taxi" }, IsRequired = true, Value = GetValue<string>(data, "VehicleType") },
                    new() { Label = "Fuel Type", Type = FieldType.Picker, Options = new List<object> { "Petrol", "Diesel", "CNG", "Electric" }, IsRequired = true, Value = GetValue<string>(data, "FuelType") },
                    new() { Label = "Seating Capacity", Type = FieldType.Number, Placeholder = "Enter seating capacity", IsRequired = true, Value = GetValue<string>(data, "SeatingCapacity") }
                });
                    break;

                case CheckListType.ServiceStationDetails:
                    formFields.AddRange(new List<FormField>
                {
                    new() { Label = "Service Type", Type = FieldType.Picker, Options = new List<object> { "General Service", "Oil Change", "Tire Replacement", "Battery Check", "Brake Inspection" }, IsRequired = true, Value = GetValue<string>(data, "ServiceType") },
                    new() { Label = "Service Details", Type = FieldType.Text, Placeholder = "Enter additional service details", IsRequired = false, Value = GetValue<string>(data, "ServiceDetails") },
                    new() { Label = "Pickup & Delivery", Type = FieldType.RadioButton, Options = new List<object> { "Yes", "No" }, IsRequired = true, Value = GetValue<string>(data, "PickupDelivery") },
                    new() { Label = "Free Pickup Distance", Type = FieldType.Number, Placeholder = "KM", IsRequired = true, Value = GetValue<string>(data, "FreePickupDistance") }
                });
                    break;

                case CheckListType.SpartPartsShopDetails:
                    formFields.AddRange(new List<FormField>
                {
                    new() { Label = "Shop Name", Type = FieldType.Text, Placeholder = "Enter shop name", IsRequired = true, Value = GetValue<string>(data, "ShopName") },
                    new() { Label = "Owner Name", Type = FieldType.Text, Placeholder = "Enter owner's full name", IsRequired = true, Value = GetValue<string>(data, "OwnerName") },
                    new() { Label = "Phone Number", Type = FieldType.Telephone, Placeholder = "Enter contact number", IsRequired = true, Value = GetValue<string>(data, "PhoneNumber") },
                    new() { Label = "Email Address", Type = FieldType.Email, Placeholder = "Enter email (optional)", IsRequired = false, Value = GetValue<string>(data, "EmailAddress") },
                    new() { Label = "Shop Address", Type = FieldType.Text, Placeholder = "Enter full address", IsRequired = true, Value = GetValue<string>(data, "ShopAddress") },
                    new() { Label = "Spare Parts Category", Type = FieldType.Picker, Options = new List<object> { "Engine Parts", "Brakes", "Tires", "Batteries", "Lighting", "Body Parts", "Accessories" }, IsRequired = true, Value = GetValue<string>(data, "SparePartsCategory") },
                    new() { Label = "Delivery Option", Type = FieldType.RadioButton, Options = new List<object> { "Yes", "No" }, IsRequired = true, Value = GetValue<string>(data, "DeliveryOption") },
                    new() { Label = "Online Ordering Available", Type = FieldType.RadioButton, Options = new List<object> { "Yes", "No" }, IsRequired = true, Value = GetValue<string>(data, "OnlineOrdering") },
                    new() { Label = "Payment Methods", Type = FieldType.Picker, Options = new List<object> { "Cash", "Credit Card", "UPI", "Bank Transfer" }, IsRequired = true, Value = GetValue<string>(data, "PaymentMethods") },
                    new() { Label = "Opening Hours", Type = FieldType.Text, Placeholder = "Enter working hours (e.g., 9 AM - 8 PM)", IsRequired = true, Value = GetValue<string>(data, "OpeningHours") },
                    new() { Label = "Additional Notes", Type = FieldType.Text, Placeholder = "Enter any extra details", IsRequired = false, Value = GetValue<string>(data, "AdditionalNotes") }
                });
                    break;
            }

            return formFields;
        }

        private static T? GetValue<T>(object? data, string propertyName)
        {
            if (data == null) return default;

            var property = data.GetType().GetProperty(propertyName);
            return property != null ? (T?)property.GetValue(data) : default;
        }

        #endregion
    }
}
