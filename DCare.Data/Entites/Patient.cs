using DCare.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Data.Entites
{
    public class Patient
    {
        public int PatientId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public string Gender { get; set; }  
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string BloodType { get; set; }
        public string Allergies { get; set; }
        public string MedicalHistory { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDelet { get; set; } = false;
        public string? ImageUrl { get; set; }
        public UserRole Role { get; set; } = UserRole.Patient;

        // Relations
        public ICollection<Appointment> Appointments { get; set; }
    }
}
