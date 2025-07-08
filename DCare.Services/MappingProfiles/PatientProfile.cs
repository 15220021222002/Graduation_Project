using AutoMapper;
using DCare.Data.Entites;
using DCare.Services.DTOs.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.MappingProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientProfileDto>();
            CreateMap<PatientUpdateDto, Patient>()
                     .ForMember(dest => dest.Email, opt => opt.Ignore());
            CreateMap<Patient, PatientDto>()
                  .ForMember(dest => dest.FullName,
                   opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<Patient, PatientDetailsDto>()
                  .ForMember(dest => dest.FullName, opt =>
                  opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

        }
    }
}
