using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.DTOs.Doctor
{
    public class DoctorBySpecializationDto
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }
        public string Address1 { get; set; }
        public string Workplace { get; set; }
        public string DoctorImgUrl { get; set; }
        public string SpecialtyName { get; set; }
    }
}
