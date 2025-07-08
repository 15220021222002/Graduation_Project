using AutoMapper;
using DCare.Data.Entites;
using DCare.Data.Enums;
using DCare.Services.DTOs.Doctor;
using DCare.Services.ImagesUrlResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.MappingProfiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<DoctorRegisterDto, Doctor>()
                   .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                   .ForMember(dest => dest.SpecialtyId, opt => opt.Ignore()) 
                   .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                   .ForMember(dest => dest.Status, opt => opt.MapFrom(src => DoctorStatus.Pending));
            CreateMap<Doctor, DoctorProfileDto>()
               .ForMember(dest => dest.Status, opt => opt.MapFrom<DoctorStatusResolver>())
               .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                //.ForMember(dest => dest.Specialization,
                // opt => opt.MapFrom(src => src.Specialty.Name))
                .ForMember(dest => dest.DoctorImgUrl,
                 opt => opt.MapFrom<DoctorImageUrlResolver>())
                .ForMember(dest => dest.LicenseImgUrl,
                 opt => opt.MapFrom<LicenseImageUrlResolver>())
                .ForMember(dest => dest.QualificationImgUrl,
                 opt => opt.MapFrom<QualificationImageUrlResolver>());
            CreateMap<DoctorProfileUpdateDto, Doctor>()
               .ForMember(dest => dest.Address1, opt => opt.Condition(src => src.Address1 != null))
               .ForMember(dest => dest.Address2, opt => opt.Condition(src => src.Address2 != null))
               .ForMember(dest => dest.Workplace, opt => opt.Condition(src => src.Workplace != null))
               .ForMember(dest => dest.AboutDoctor, opt => opt.Condition(src => src.AboutDoctor != null))
               .ForMember(dest => dest.YearsOfExperience, opt => opt.Condition(src => src.YearsOfExperience != null))
               .ForMember(dest => dest.fees, opt => opt.Condition(src => src.fees != null));
      
            CreateMap<Doctor, DoctorAdminListDto>()
                  .ForMember(dest => dest.FullName,
                   opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<Doctor, DoctorPendingListDto>()
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialty.Name))
                .ForMember(dest => dest.DoctorImgUrl,
                 opt => opt.MapFrom<DoctorImageUrlResolver2>())
                .ForMember(dest => dest.LicenseImgUrl,
                 opt => opt.MapFrom<LicenseImageUrlResolver2>())
                .ForMember(dest => dest.QualificationImgUrl,
                 opt => opt.MapFrom<QualificationImageUrlResolver2>());

            CreateMap<Doctor, DoctorBySpecializationDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.SpecialtyName, opt => opt.MapFrom(src => src.Specialty.Name))
                .ForMember(dest => dest.DoctorImgUrl, opt => opt.MapFrom<DoctorImageUrlResolver1>());
            CreateMap<Doctor, DoctorOverviewDto>()
               .ForMember(dest => dest.FullName,
               opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            //CreateMap<Doctor, DoctorProfileDto>()
            //.ForMember(dest => dest.Status, opt => opt.MapFrom<DoctorStatusResolver>());


        }

    }
}
