﻿
using ZTaxiApp.Model.DTOs;

namespace ZTaxiApp.Model.Request
{
    public class VendorRegisterRequest
    {
        #region Properties

        public string? phoneNumber { get; set; }

        public UserDetailDto? UserDetails { get; set; }

        public VehicleDto? VehicleDetails { get; set; }

        public required VendorDetailDto VendorDetails { get; set; }

        #endregion
    }
}
