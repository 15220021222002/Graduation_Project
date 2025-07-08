using DCare.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Data.Entites
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public decimal Amount { get; set; }
        //public decimal PlatformFee { get; set; }
        //public decimal DoctorFee { get; set; }

        public string SessionId { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Held; // Enum: Held, Completed, Refunded
        public string ?TransactionId { get; set; }
        public bool IsManuallyRefunded { get; set; } = false;

        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime? PaidAt { get; set; }
        public DateTime? UpdatedAt { get; set; }=DateTime.Now;
    }
}
