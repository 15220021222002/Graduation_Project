using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.DTOs.Appointment
{
    public class ConfirmAppointmentResponseDto
    {
        public string Message { get; set; }
        public decimal DoctorShare { get; set; }
        public decimal PlatformShare { get; set; }
    }
}
