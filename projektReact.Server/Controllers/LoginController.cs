using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projektReact.Server.Interfaces;
using projektReact.Server.RequestModels;

namespace projektReact.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        public const string AuthCookieName = "jwt";

        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel login, CancellationToken ct)
        {
            var result = await _authService.LoginAsync(login.email, login.password, ct);
            if (result == null)
            {
                return Unauthorized(new { message = "Nieprawidłowy email lub hasło" });
            }

            Response.Cookies.Append(AuthCookieName, result.Token.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = result.Token.Expires
            });

            return Ok(new { username = result.User.Username, role = result.User.Role });
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                username = User.Identity?.Name,
                role = User.FindFirstValue(ClaimTypes.Role)
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(AuthCookieName, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
            return Ok();
        }
    }
}
