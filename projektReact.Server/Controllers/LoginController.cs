using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using projektReact.Server.ProjektWPF.Data;
using projektReact.Server.RequestModels;
using projektReact.Server.DataModels;

namespace projektReact.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        public const string AuthCookieName = "jwt";
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromMinutes(30);

        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        public LoginController(IConfiguration config, AppDbContext dbContext)
        {
            _config = config;
            _context = dbContext;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var user = _context.Users.FirstOrDefault(p => p.Username == login.email);
            if (user == null)
            {
                return Unauthorized(new { message = "Nieprawidłowy email lub hasło" });
            }

            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, login.password);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Nieprawidłowy email lub hasło" });
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.Add(TokenLifetime);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            Response.Cookies.Append(AuthCookieName, new JwtSecurityTokenHandler().WriteToken(token), new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = expires
            });

            return Ok(new { username = user.Username, role = user.Role });
        }

        [HttpGet("me")]
        [Authorize]
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
