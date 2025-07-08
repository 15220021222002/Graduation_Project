using AutoMapper;
using DCare.Data.Entites;
using DCare.Repository.Implementations;
using DCare.Repository.Interfaces;
using DCare.Services.DTOs.Availability;
using DCare.Services.DTOs.Doctor;
using DCare.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.Implementations
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly IDoctorAvailabilityRepository _availabilityRepository;
        private readonly IMapper _mapper;


        public DoctorAvailabilityService(IDoctorAvailabilityRepository availabilityRepository, IMapper mapper)
        {
            _availabilityRepository = availabilityRepository;
            _mapper = mapper;
        }

        public async Task AddAvailabilityAsync(CreateAvailabilityDto dto)
        {
            var availability = _mapper.Map<DoctorAvailability>(dto);

            await _availabilityRepository.AddAsync(availability);
            await _availabilityRepository.SaveChangesAsync();
        }
        public async Task<IEnumerable<DoctorAvailability>> GetAvailableAppointmentsByDoctorIdAsync(int doctorId)
        {
            return await _availabilityRepository.GetAvailableAppointmentsByDoctorIdAsync(doctorId);
        }
        public async Task<bool> DeleteAvailabilityAsync(int availabilityId, int doctorId)
        {
            var availability = await _availabilityRepository.GetByIdAsync(availabilityId);

            if (availability == null || availability.DoctorId != doctorId)
                return false;

            await _availabilityRepository.DeleteAsync(availability);
            return true;
        }
        public async Task<IEnumerable<DoctorAvailabilityDetailsDto>> GetAllForDoctorAsync(int doctorId)
        {
            var availabilities = await _availabilityRepository.GetAllByDoctorIdAsync(doctorId);
            return _mapper.Map<IEnumerable<DoctorAvailabilityDetailsDto>>(availabilities);
        }



    }
}
