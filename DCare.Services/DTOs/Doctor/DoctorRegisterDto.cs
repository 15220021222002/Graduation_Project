using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.DTOs.Doctor
{
    public class DoctorRegisterDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Specialization { get; set; }

        public int YearsOfExperience { get; set; }

        public string Workplace { get; set; }

        public IFormFile LicenseImg { get; set; }

        public IFormFile QualificationImg { get; set; }

        public IFormFile DoctorImg { get; set; }

        public double Fees { get; set; }

        public string ProfessionalTitle { get; set; }

        public string AboutDoctor { get; set; }

        public string Password { get; set; }
    }
}