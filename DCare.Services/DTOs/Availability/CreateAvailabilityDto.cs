using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.DTOs.Availability
{
    public class CreateAvailabilityDto
    {
        public int DoctorId { get; set; }
        public string DayOfWeek { get; set; } // مثلاً "Sunday"
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
