using projektReact.Server.DataModels;

namespace projektReact.Server.Interfaces
{
    public record LoginResult(User User, TokenResult Token);

    public interface IAuthService
    { 
        Task<LoginResult?> LoginAsync(string email, string password, CancellationToken ct = default); 
        Task<bool> RegisterAsync(string username, string password, CancellationToken ct = default);
    }
}
