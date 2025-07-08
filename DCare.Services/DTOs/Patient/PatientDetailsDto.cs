using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.DTOs.Patient
{
    public class PatientDetailsDto
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string BloodType { get; set; }
        public string Allergies { get; set; }
        public string MedicalHistory { get; set; }
    }
}
