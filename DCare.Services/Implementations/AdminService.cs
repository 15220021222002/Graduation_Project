using DCare.Repository.Interfaces;
using DCare.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IPaymentRepository _paymentRepository;

        public AdminService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<decimal> GetPlatformEarningsAsync()
        {
            return await _paymentRepository.GetTotalPlatformEarningsAsync();
        }
    }
}
