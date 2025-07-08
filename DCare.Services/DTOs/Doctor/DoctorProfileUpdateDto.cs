using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.DTOs.Doctor
{
    public class DoctorProfileUpdateDto
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public double fees { get; set; }
        public string Workplace { get; set; }
        public int YearsOfExperience { get; set; }
        public string AboutDoctor { get; set; }
    }
}
