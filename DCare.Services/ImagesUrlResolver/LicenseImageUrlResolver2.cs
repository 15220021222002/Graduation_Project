using AutoMapper;
using DCare.Data.Entites;
using DCare.Services.DTOs.Doctor;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.ImagesUrlResolver
{
    public class LicenseImageUrlResolver2 : IValueResolver<Doctor, DoctorPendingListDto, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LicenseImageUrlResolver2(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(Doctor source, DoctorPendingListDto destination, string destMember, ResolutionContext context)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (string.IsNullOrEmpty(source.LicenseImgUrl)) return string.Empty;
            return $"{request.Scheme}://{request.Host}/{source.LicenseImgUrl}";
        }
    }
}
