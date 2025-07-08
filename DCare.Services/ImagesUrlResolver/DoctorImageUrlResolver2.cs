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
    public class DoctorImageUrlResolver2 : IValueResolver<Doctor, DoctorPendingListDto, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DoctorImageUrlResolver2(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(Doctor source, DoctorPendingListDto destination, string destMember, ResolutionContext context)
        {
            var request = _httpContextAccessor.HttpContext?.Request;

            if (request == null || string.IsNullOrEmpty(source.DoctorImgUrl))
                return null!;

            var baseUrl = $"{request.Scheme}://{request.Host}";
            return $"{baseUrl}/{source.DoctorImgUrl.Replace("\\", "/")}";
        }
    }
}
