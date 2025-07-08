using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.DTOs.Appointment
{
    public class BookAppointmentDto
    {
        public int DoctorId { get; set; }
        public int AvailabilityId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}
