using DCare.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.DTOs.Appointment
{
    public class AdminAppointmentDto
    {
        public int AppointmentId { get; set; }
        public string PatientFullName { get; set; }
        public string DoctorFullName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; }
    }
}
