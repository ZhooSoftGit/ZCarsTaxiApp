using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.UIModel
{
    public class Driver
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
    }
    public class Vehicle
    {
        public string RegistrationNumber { get; set; }
        public string AssignmentStatus { get; set; }
        public string CurrentLocation { get; set; }
    }

    public class VehicleDetail
    {
        public string Icon { get; set; }
        public string Value { get; set; }
    }

    public class VehiclePricing
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class RideModel
    {
        public string CabImage { get; set; }
        public string CabNumber { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string Distance { get; set; }
        public decimal Fare { get; set; }
        public string DriverName { get; set; }
        public string DriverPhoto { get; set; }
    }

    public class RideReview
    {
        public string UserName { get; set; }
        public string UserPhoto { get; set; }
        public string ReviewDate { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
    }
}
