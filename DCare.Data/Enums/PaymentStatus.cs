using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Data.Enums
{
    public enum PaymentStatus
    {
        Held,       // في انتظار الدفع
        Completed,  // تم الدفع
        Refunded    // تم استرجاع المبلغ
    }
}
