using AutoMapper;
using DCare.Data.Entites;
using DCare.Services.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.MappingProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<BookAppointmentDto, Appointment>()
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.PatientId, opt => opt.Ignore())
                .ForMember(dest => dest.AppointmentId, opt => opt.Ignore())
                .ForMember(dest => dest.Doctor, opt => opt.Ignore())
                .ForMember(dest => dest.Patient, opt => opt.Ignore())
                .ForMember(dest => dest.Availability, opt => opt.Ignore())
                .ForMember(dest => dest.Payment, opt => opt.Ignore());
            CreateMap<Appointment, AdminAppointmentDto>()
             .ForMember(dest => dest.PatientFullName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
             .ForMember(dest => dest.DoctorFullName, opt => opt.MapFrom(src => src.Doctor.FirstName + " " + src.Doctor.LastName))
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<Appointment, DoctorAppointmentDto>()
             .ForMember(dest => dest.PatientFullName, opt =>
             opt.MapFrom(src => $"{src.Patient.FirstName} {src.Patient.LastName}"))
             .ForMember(dest => dest.PatientId, opt =>
               opt.MapFrom(src => src.PatientId))
             .ForMember(dest => dest.AppointmentId,
               opt => opt.MapFrom(src => src.AppointmentId))
             .ForMember(dest => dest.Status, opt =>
             opt.MapFrom(src => src.Status.ToString()));
            CreateMap<Appointment, PatientAppointmentDto>()
                .ForMember(dest => dest.DoctorFullName, opt =>
                opt.MapFrom(src => $"{src.Doctor.FirstName} {src.Doctor.LastName}"))
               .ForMember(dest => dest.DoctorPhoneNumber, opt =>
                opt.MapFrom(src => src.Doctor.PhoneNumber))
               .ForMember(dest => dest.AppointmentId,
                opt => opt.MapFrom(src => src.AppointmentId))
               .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => src.Status.ToString()));


        }
    }
}
