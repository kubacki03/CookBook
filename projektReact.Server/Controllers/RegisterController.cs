using Microsoft.AspNetCore.Mvc;
using projektReact.Server.Interfaces;
using projektReact.Server.RequestModels;

namespace projektReact.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IAuthService _authService;

        public RegisterController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel, CancellationToken ct)
        {
            var created = await _authService.RegisterAsync(registerModel.Username, registerModel.Password, ct);
            if (!created)
            {
                return Conflict(new { message = "Użytkownik o takim emailu już istnieje" });
            }

            return Ok();
        }
    }
}
