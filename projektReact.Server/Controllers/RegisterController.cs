using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using projektReact.Server.DataModels;
using projektReact.Server.ProjektWPF.Data;
using projektReact.Server.RequestModels;

namespace projektReact.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : Controller
    {

        private readonly AppDbContext _context;

        public RegisterController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            var user = _context.Users.FirstOrDefault(p => p.Username == registerModel.Username);
            if (user != null)
            {
                return Conflict();
            }
            var passwordHasher = new PasswordHasher<User>();
            var hashedPassword = passwordHasher.HashPassword(user, registerModel.Password);
            var newUser = new User { Password = hashedPassword, Username = registerModel.Username };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok();
        }
    }
}
