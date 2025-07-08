using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DCare.Services.Interfaces; // تأكدي من إضافة النيمسبيس الصح

namespace DCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("earnings")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPlatformEarnings()
        {
            var earnings = await _adminService.GetPlatformEarningsAsync();
            return Ok(new { PlatformEarnings = earnings });
        }
    }
}

