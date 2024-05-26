using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using mmp_prj.Models;
using mmp_prj.Service;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace mmp_prj.Controllers
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

        [HttpPost("login")]
        public IActionResult Login(LoginModel user)
        {
            var userLogin = _authService.Authenticate(user.Email, user.Password);
            if (userLogin == null)
                return BadRequest("Invalid email or password");

            var token = _authService.GenerateToken(userLogin);
            return Ok(new { Token = token , Role = userLogin.Role});
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterModel user)
        { // Check if username is already registered
            if (_authService.UserExists(user.Email))
            {
                return Conflict(new { Error = "Username already exists" });
            }

            // Register user
            _authService.Register(user.Email, user.Password,user.Role);

            return Ok(new { Message = "User registered successfully" });
        }
    }
}
