using DCare.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Data.Entites
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PasswordHash { get; set; }
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
        public int YearsOfExperience { get; set; }
        public string Workplace { get; set; }
        //Edite Name to LicenseImgUrl
        public string LicenseImgUrl { get; set; }
        //Edite Name to QualificationImgUrl
        public string QualificationImgUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DoctorStatus Status { get; set; } = DoctorStatus.Pending; //Pending ,Approved ,Rejected
        public bool IsDeleted { get; set; }=false;
        public string? REjectionReason { get; set; }
        //Edite name to DoctorImgUrl
        public string DoctorImgUrl { get; set; }
        //fees
        [Column(TypeName ="money")]
        public double fees { get; set; }
        public UserRole Role { get; set; } = UserRole.Doctor;
        public string ProfessionalTitle { get; set; } // ✅ New property added here
        public string ?AboutDoctor { get; set; }

        // Relations
        public ICollection<DoctorAvailability> Availabilities { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
