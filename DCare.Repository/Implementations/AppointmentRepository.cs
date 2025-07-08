using DCare.Data.Context;
using DCare.Data.Entites;
using DCare.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DCare.Data.Enums;

namespace DCare.Repository.Implementations
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DCareDbContext _context;

        public AppointmentRepository(DCareDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
        }

        public async Task<bool> IsSlotTakenAsync(int doctorId, int availabilityId, DateTime date, TimeSpan time)
        {
            return await _context.Appointments.AnyAsync(a =>
                a.DoctorId == doctorId &&
                a.AvailabilityId == availabilityId &&
                a.Date.Date == date.Date &&
                a.Time == time
            );
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<Appointment>> GetAllAppointmentsWithDetailsAsync()
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
        }
        public async Task<List<Appointment>> GetPaidAppointmentsForDoctorAsync(int doctorId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Payment)
                .Where(a => a.DoctorId == doctorId
                    && a.Payment != null
                 && a.Payment.Status == PaymentStatus.Completed)
                .ToListAsync();
        }
        public async Task<List<Appointment>> GetAppointmentsForPatientAsync(int patientId)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Payment)
                .Where(a =>
                    a.PatientId == patientId
                    &&
                    a.Payment != null &&
                    a.Payment.Status == PaymentStatus.Completed)
                .ToListAsync();
        }
        public async Task<Appointment> GetByIdAsync(int appointmentId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Appointment> GetAppointmentWithPatientAsync(int appointmentId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
        }

        public async Task DeleteExpiredAppointmentsAsync()
        {
            var threshold = DateTime.UtcNow.AddMinutes(-10);

            var expiredAppointments = await _context.Appointments
                .Where(a =>
                    a.Status == AppointmentStatus.Pending &&
                    a.CreatedAt <= threshold &&
                    a.Payment != null &&
                    a.Payment.Status == PaymentStatus.Held
                )
                .ToListAsync();

            if (expiredAppointments.Any())
            {
                _context.Appointments.RemoveRange(expiredAppointments);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Appointment> GetAppointmentWithPaymentAndDoctorAsync(int appointmentId)
        {
            return await _context.Appointments
                .Include(a => a.Payment)
                .Include(a => a.Doctor)
                .Include(a => a.Availability)
                .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
        }

        public async Task<decimal> GetDoctorTotalEarningsAsync(int doctorId)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId &&
                            a.Status == AppointmentStatus.Completed &&
                            a.Payment != null &&
                            a.Payment.Status == PaymentStatus.Completed)
                .SumAsync(a => a.Payment.Amount * 0.8m); // 80% للدكتور
        }

        public async Task<int> GetDoctorConfirmedAppointmentsCountAsync(int doctorId)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId &&
                            a.Status == AppointmentStatus.Completed &&
                            a.Payment != null &&
                            a.Payment.Status == PaymentStatus.Completed)
                .CountAsync();
        }



    }
}
