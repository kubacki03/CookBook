using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using projektReact.Server.DataModels;
using projektReact.Server.Interfaces;
using projektReact.Server.ProjektWPF.Data;

namespace projektReact.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthService(AppDbContext context, IPasswordHasher<User> passwordHasher, ITokenService tokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<LoginResult?> LoginAsync(string email, string password, CancellationToken ct = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == email, ct);
            if (user == null)
            {
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return new LoginResult(user, _tokenService.CreateToken(user));
        }

        public async Task<bool> RegisterAsync(string username, string password, CancellationToken ct = default)
        {
            var exists = await _context.Users.AnyAsync(p => p.Username == username, ct);
            if (exists)
            {
                return false;
            }

            var newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Role = Roles.User
            };
            newUser.Password = _passwordHasher.HashPassword(newUser, password);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}
