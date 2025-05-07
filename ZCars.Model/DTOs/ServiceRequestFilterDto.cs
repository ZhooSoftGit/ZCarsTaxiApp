using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhooCars.Model.DTOs
{
    public class ServiceRequestFilterDto
    {
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public int? AssignedProviderId { get; set; }
        public string? Name { get; set; }
        public int? Status { get; set; }
    }

}
