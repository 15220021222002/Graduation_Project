using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.DTOs.Patient
{
    public class PatientUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        //public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string BloodType { get; set; }
        public string Allergies { get; set; }
        public string MedicalHistory { get; set; }
    }
}
