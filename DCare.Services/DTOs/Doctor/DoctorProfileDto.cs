using DCare.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.DTOs.Doctor
{
    public class DoctorProfileDto
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        //public string Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public string Workplace { get; set; }
        public double Fees { get; set; }
        public string LicenseImgUrl { get; set; }
        public string QualificationImgUrl { get; set; }
        public string DoctorImgUrl { get; set; }
        public string ProfessionalTitle { get; set; }
        public string? AboutDoctor { get; set; }
        public string Status { get; set; } 
    }
}

