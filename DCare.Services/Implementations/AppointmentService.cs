using AutoMapper;
using DCare.Data.Entites;
using DCare.Data.Enums;
using DCare.Repository.Implementations;
using DCare.Repository.Interfaces;
using DCare.Services.Configuration;
using DCare.Services.DTOs.Appointment;
using DCare.Services.DTOs.Doctor;
using DCare.Services.DTOs.Patient;
using DCare.Services.Interfaces;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.Implementations
{
    public class AppointmentService:IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly StripeSettings _stripeSettings;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper, IDoctorRepository doctorRepository, IPaymentRepository paymentRepository, IOptions<StripeSettings> stripeOptions)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _doctorRepository = doctorRepository;
            _paymentRepository = paymentRepository;
            _stripeSettings = stripeOptions.Value;
        }

        public async Task<string> BookAppointmentAsync(BookAppointmentDto dto, int patientId)
        {
            var isTaken = await _appointmentRepository.IsSlotTakenAsync(dto.DoctorId, dto.AvailabilityId, dto.Date, dto.Time);
            if (isTaken)
                throw new Exception("This slot is already booked.");

            var appointment = _mapper.Map<Appointment>(dto);
            appointment.PatientId = patientId;
            appointment.Status = AppointmentStatus.Pending;
            appointment.CreatedAt = DateTime.UtcNow;
            appointment.ExpiresAt = DateTime.UtcNow.AddMinutes(10);

            var doctor = await _doctorRepository.GetByIdAsync(dto.DoctorId);
            if (doctor == null)
                throw new Exception("Doctor not found.");

            var amount = (decimal)doctor.fees;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
    {
        new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmountDecimal = amount * 100, // Stripe uses cents
                Currency = "usd",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = $"Consultation with Dr. {doctor.FirstName} {doctor.LastName}"
                }
            },
            Quantity = 1
        }
    },
                Mode = "payment",
                SuccessUrl = _stripeSettings.SuccessUrl,
                CancelUrl = _stripeSettings.CancelUrl
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync(); // احفظ الموعد

            var payment = new Payment
            {
                AppointmentId = appointment.AppointmentId,
                Amount = amount,
                SessionId = session.Id,
                Status = PaymentStatus.Held,
                CreatedAt = DateTime.UtcNow
            };

            await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync(); // احفظ الدفع

            return session.Id;
        }

        public async Task<IEnumerable<AdminAppointmentDto>> GetAllAppointmentsForAdminAsync()
        {
            var appointments = await _appointmentRepository.GetAllAppointmentsWithDetailsAsync();
            return _mapper.Map<IEnumerable<AdminAppointmentDto>>(appointments);
        }
        public async Task<List<DoctorAppointmentDto>> GetDoctorAppointmentsAsync(int doctorId)
        {
            var appointments = await _appointmentRepository.GetPaidAppointmentsForDoctorAsync(doctorId);
            return _mapper.Map<List<DoctorAppointmentDto>>(appointments);
        }
        public async Task<IEnumerable<PatientAppointmentDto>> GetAppointmentsForPatientAsync(int patientId)
        {
            var appointments = await _appointmentRepository.GetAppointmentsForPatientAsync(patientId);
            return _mapper.Map<IEnumerable<PatientAppointmentDto>>(appointments);
        }
        public async Task<bool> CancelAppointmentAsync(int appointmentId, int userId, string role)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
                return false;

            // Check if user is authorized (doctor or patient who owns the appointment)
            if (role == "Patient" && appointment.PatientId != userId)
                return false;

            if (role == "Doctor" && appointment.DoctorId != userId)
                return false;

            // Cancel the appointment
            appointment.Status = AppointmentStatus.Canceled;
            await _appointmentRepository.SaveAsync();

            return true;
        }
        public async Task<PatientDetailsDto> GetPatientDetailsByAppointmentIdAsync(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetAppointmentWithPatientAsync(appointmentId);

            if (appointment == null || appointment.Patient == null)
                throw new Exception("Appointment or patient not found");

            return _mapper.Map<PatientDetailsDto>(appointment.Patient);
        }
        public async Task<ConfirmAppointmentResponseDto> ConfirmAppointmentAsync(int appointmentId)
        {
            // 1. جيب الموعد + الدفع المرتبط به
            var appointment = await _appointmentRepository.GetAppointmentWithPaymentAndDoctorAsync(appointmentId);

            if (appointment == null)
                throw new Exception("Appointment not found.");

            if (appointment.Status != AppointmentStatus.Pending)
                throw new Exception("Appointment is already confirmed.");

            var payment = appointment.Payment;

            if (payment == null || payment.Status != PaymentStatus.Completed)
                throw new Exception("Payment must be completed before confirming the appointment.");

            // 2. حدّث حالة الموعد
            appointment.Status = AppointmentStatus.Completed;

            if (appointment.Availability != null && appointment.Availability.Status == AvailabilityStatus.Booked)
            {
                appointment.Availability.Status = AvailabilityStatus.Available;
            }
            await _appointmentRepository.SaveChangesAsync();

            // 3. احسب النسب
            var total = payment.Amount;
            var doctorShare = total * 0.8M;
            var platformShare = total * 0.2M;

            // 4. رجّعهم في DTO
            return new ConfirmAppointmentResponseDto
            {
                Message = "Appointment confirmed successfully.",
                DoctorShare = doctorShare,
                PlatformShare = platformShare
            };
        }

        public async Task<DoctorEarningsDto> GetDoctorEarningsAsync(int doctorId)
        {
            var total = await _appointmentRepository.GetDoctorTotalEarningsAsync(doctorId);
            var count = await _appointmentRepository.GetDoctorConfirmedAppointmentsCountAsync(doctorId);

            return new DoctorEarningsDto
            {
                TotalEarnings = total,
                ConfirmedAppointmentsCount = count
            };
        }



    }
}
