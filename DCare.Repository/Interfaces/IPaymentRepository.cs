using DCare.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Repository.Interfaces
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
        Task<Payment> GetBySessionIdAsync(string sessionId);
        Task SaveChangesAsync();
        Task DeletePaymentsForExpiredAppointmentsAsync();
        Task<Payment> GetPaymentWithAppointmentAndAvailabilityBySessionIdAsync(string sessionId);
        Task<decimal> GetTotalPlatformEarningsAsync();


    }
}
