using AutoMapper;
using DCare.Data.Entites;
using DCare.Data.Enums;
using DCare.Services.DTOs.Availability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.MappingProfiles
{
    public class DoctorAvailabilityProfile : Profile
    {
        public DoctorAvailabilityProfile()
        {
            CreateMap<CreateAvailabilityDto, DoctorAvailability>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => AvailabilityStatus.Available));
            CreateMap<DoctorAvailability, DoctorAvailabilityDetailsDto>();
    //        CreateMap<DoctorAvailability, DoctorAvailabilityDto>()
    //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));


        }
    }
}
