using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhooCars.Model.DTOs
{
    public class PeakHourDto
    {
        public int Id { get; set; }
        public string PeakDay { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsWeekend { get; set; }
        public bool IsHoliday { get; set; }
        public decimal PeakMultiplier { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateUpdatePeakHourDto
    {
        [Required]
        public string PeakDay { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public bool IsWeekend { get; set; } = false;
        public bool IsHoliday { get; set; } = false;

        [Range(1.0, 10.0, ErrorMessage = "Multiplier must be between 1.0 and 10.0")]
        public decimal PeakMultiplier { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
