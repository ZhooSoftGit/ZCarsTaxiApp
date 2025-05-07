using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class ServiceRequestDto
    {
        public int TicketId { get; set; }

        public int? UserId { get; set; }

        public string Name { get; set; }

        public string? Email { get; set; }

        public string Phone { get; set; }

        public string Subject { get; set; }

        public string ServiceType { get; set; }

        public string VehicleType { get; set; }

        public string IssueDetails { get; set; }

        public string Description { get; set; }

        
        public string DoorNo { get; set; }

        public string Street1 { get; set; }

        public string? Street2 { get; set; }

        public string City { get; set; }

        public string Pincode { get; set; }

        public int Status { get; set; } // (0 = Open, 1 = In Progress, 2 = Closed)

        public string Priority { get; set; }

        public bool PickupRequired {  get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }


    public class CreateServiceRequestDto
    {
        [Required]
        public int? UserId { get; set; }  // Optional for guest users

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string ServiceType { get; set; } // Repair, Maintenance, etc.

        [Required]
        public string VehicleType { get; set; } // Hatchback, Sedan, SUV, etc.

        [Required]
        public string IssueDetails { get; set; } // "Brake failure, Oil leak, etc."

        [Required]
        public string Description { get; set; }

        // Address Fields
        [Required]
        public string DoorNo { get; set; }

        [Required]
        public string Street1 { get; set; }

        public string? Street2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Pincode { get; set; }

        public string? Priority { get; set; } = "Medium"; // Default Priority

        public bool PickupRequired { get; set; }
    }



    public class UpdateStatusDto
    {
        public ServiceRequestStatus Status { get; set; }
    }


    public class ServiceRequestDetailsDto
    {
        public ServiceRequestDto? ServiceRequest { get; set; }

        public ServiceRequestAssignmentDto? ServiceRequestAssignment { get; set; }

        public ServiceRequestPickupDropDto? RequestPickupDropDto { get; set; }

    }


    public class ServiceRequestAssignmentDto
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public int? AssignedProviderId { get; set; }

        public DateTime? AssignedAt { get; set; }

        public DateTime? EstimatedCompletionTime { get; set; }

        public DateTime? CompletedAt { get; set; }
    }

    public class ServiceRequestPickupDropDto
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public bool PickupRequired { get; set; }

        public string? PickupBy { get; set; } // 'Owner' or 'Service Provider'

        public DateTime? PickupTime { get; set; }

        public string? PickupAddress { get; set; }

        public decimal? PickupLatitude { get; set; }

        public decimal? PickupLongitude { get; set; }

        public string? DropBy { get; set; } // 'Owner' or 'Service Provider'

        public DateTime? DropTime { get; set; }

        public string? DropAddress { get; set; }

        public decimal? DropLatitude { get; set; }

        public decimal? DropLongitude { get; set; }
    }



}
