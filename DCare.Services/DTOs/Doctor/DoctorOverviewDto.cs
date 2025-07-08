using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.DTOs.Doctor
{
    public class DoctorOverviewDto
    {
        public string FullName { get; set; }
        public string AboutDoctor { get; set; }
        public string Workplace { get; set; }
        public double Fees { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
    }
}
