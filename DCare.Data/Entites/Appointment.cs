using DCare.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Data.Entites
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int AvailabilityId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DoctorAvailability Availability { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public DateTime CreatedAt { get; set; }
        public Payment Payment { get; set; }
    }
}
