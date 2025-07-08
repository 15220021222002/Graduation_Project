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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DCareDbContext _context;

        public PaymentRepository(DCareDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }

        public async Task<Payment> GetBySessionIdAsync(string sessionId)
        {
            return await _context.Payments
                .Include(p => p.Appointment)
                .FirstOrDefaultAsync(p => p.SessionId == sessionId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task DeletePaymentsForExpiredAppointmentsAsync()
        {
            var threshold = DateTime.UtcNow.AddMinutes(-10);

            var expiredPayments = await _context.Payments
                .Where(p =>
                    p.Status == PaymentStatus.Held &&
                    p.Appointment != null &&
                    p.Appointment.Status == AppointmentStatus.Pending &&
                    p.Appointment.CreatedAt <= threshold
                )
                .ToListAsync();

            if (expiredPayments.Any())
            {
                _context.Payments.RemoveRange(expiredPayments);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Payment> GetPaymentWithAppointmentAndAvailabilityBySessionIdAsync(string sessionId)
        {
            return await _context.Payments
                .Include(p => p.Appointment)
                    .ThenInclude(a => a.Availability)
                .FirstOrDefaultAsync(p => p.SessionId == sessionId);
        }
        public async Task<decimal> GetTotalPlatformEarningsAsync()
        {
            var totalCompletedPayments = await _context.Payments
                .Where(p => p.Status == PaymentStatus.Completed)
                .SumAsync(p => p.Amount);

            return totalCompletedPayments * 0.2M; // 20% للمنصة
        }


    }
}
