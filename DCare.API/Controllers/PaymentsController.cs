using DCare.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("confirm")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> ConfirmPayment([FromBody] ConfirmPaymentRequest request)
        {
            await _paymentService.ConfirmPaymentAsync(request.SessionId);
            return Ok(new { message = "Payment confirmed successfully." });
        }
    }

    public class ConfirmPaymentRequest
    {
        public string SessionId { get; set; }
    }
}
