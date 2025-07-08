using AutoMapper;
using DCare.Data.Entites;
using DCare.Services.DTOs.Specialty;
using DCare.Services.ImagesUrlResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.MappingProfiles
{
    public class SpecialtyProfile: Profile
    {
        public SpecialtyProfile()
        {
            CreateMap<Specialty, SpecialtyDto>()
                .ForMember(dest => dest.ImageUrl, opt =>
                    opt.MapFrom<SpecialtyUrlResolver>());
        }
    }
}
