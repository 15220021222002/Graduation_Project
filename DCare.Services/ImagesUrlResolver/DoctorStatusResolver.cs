using AutoMapper;
using DCare.Data.Entites;
using DCare.Services.DTOs.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.ImagesUrlResolver
{
    public class DoctorStatusResolver : IValueResolver<Doctor, DoctorProfileDto, string>
    {
        public string Resolve(Doctor source, DoctorProfileDto destination, string destMember, ResolutionContext context)
        {
            return source.Status.ToString(); // يحول enum إلى string
        }
    }
}
