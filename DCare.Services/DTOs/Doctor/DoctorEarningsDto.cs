using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.DTOs.Doctor
{
    public class DoctorEarningsDto
    {
        public decimal TotalEarnings { get; set; } 
        public int ConfirmedAppointmentsCount { get; set; }
    }
}
