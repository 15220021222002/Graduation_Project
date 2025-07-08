using AutoMapper;
using DCare.Data.Entites;
using DCare.Services.DTOs.Specialty;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.ImagesUrlResolver
{
    public class SpecialtyUrlResolver : IValueResolver<Specialty, SpecialtyDto, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SpecialtyUrlResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(Specialty source, SpecialtyDto destination, string destMember, ResolutionContext context)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            // source.ImageUrl contains: "images/Specialties/photo_2.jpg"
            return $"{baseUrl}/{source.ImageUrl}";
           
            
        }
        
    }
}
