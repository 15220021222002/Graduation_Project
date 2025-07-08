using DCare.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Repository.Interfaces
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);
        Task<bool> IsSlotTakenAsync(int doctorId, int availabilityId, DateTime date, TimeSpan time);
        Task SaveChangesAsync();
        Task<List<Appointment>> GetAllAppointmentsWithDetailsAsync();
        Task<List<Appointment>> GetPaidAppointmentsForDoctorAsync(int doctorId);
        Task<List<Appointment>> GetAppointmentsForPatientAsync(int patientId);
        Task<Appointment> GetByIdAsync(int appointmentId);
        Task SaveAsync();
        Task<Appointment> GetAppointmentWithPatientAsync(int appointmentId);
        Task DeleteExpiredAppointmentsAsync();
        Task<Appointment> GetAppointmentWithPaymentAndDoctorAsync(int appointmentId);
        Task<decimal> GetDoctorTotalEarningsAsync(int doctorId);
        Task<int> GetDoctorConfirmedAppointmentsCountAsync(int doctorId);


    }
}
