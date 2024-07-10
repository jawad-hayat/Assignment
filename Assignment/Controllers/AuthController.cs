using Business.Interfaces;
using Infrastructure.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LogInRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Login(request);
                return Ok(result);
            }
            return NotFound();
        }
    }
}
