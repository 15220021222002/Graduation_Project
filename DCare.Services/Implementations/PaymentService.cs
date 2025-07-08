using DCare.Data.Enums;
using DCare.Repository.Interfaces;
using DCare.Services.Interfaces;
using Stripe.Checkout;
using System;
using System.Threading.Tasks;

namespace DCare.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task ConfirmPaymentAsync(string sessionId)
        {
            // 1. جيب الـ Payment + Appointment + Availability
            var payment = await _paymentRepository.GetPaymentWithAppointmentAndAvailabilityBySessionIdAsync(sessionId);

            if (payment == null)
                throw new Exception("Invalid session ID");

            // 2. اتأكد إن الدفع مش مكرر
            if (payment.Status == PaymentStatus.Completed)
                return;

            // 3. اتأكد من Stripe إن الدفع تم فعلاً
            var service = new SessionService();
            Session session;
            try
            {
                session = await service.GetAsync(sessionId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Stripe error: {ex.Message}", ex); // هيظهر الخطأ الحقيقي هنا
            }

            if (session.PaymentStatus != "paid")
                throw new Exception("Payment not completed yet.");

            // 4. حدّث الدفع
            payment.Status = PaymentStatus.Completed;
            payment.PaidAt = DateTime.UtcNow;
            payment.UpdatedAt = DateTime.UtcNow;

            // 5. حدّث المعاد
            if (payment.Appointment?.Availability != null)
            {
                payment.Appointment.Availability.Status = AvailabilityStatus.Booked;
            }

            await _paymentRepository.SaveChangesAsync();
        }

    }
}
