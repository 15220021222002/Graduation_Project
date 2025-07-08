using DCare.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.BackgroundJobs
{
    public class ExpiredAppointmentCleaner : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ExpiredAppointmentCleaner> _logger;

        public ExpiredAppointmentCleaner(IServiceProvider serviceProvider, ILogger<ExpiredAppointmentCleaner> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    try
                    {
                        var appointmentRepo = scope.ServiceProvider.GetRequiredService<IAppointmentRepository>();
                        var paymentRepo = scope.ServiceProvider.GetRequiredService<IPaymentRepository>();

                        await appointmentRepo.DeleteExpiredAppointmentsAsync();
                        await paymentRepo.DeletePaymentsForExpiredAppointmentsAsync();

                        _logger.LogInformation("Checked and cleaned expired appointments at: {Time}", DateTime.Now);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error while cleaning expired appointments.");
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); 
            }
        }
    }
}

