using projektReact.Server.DataModels;

namespace projektReact.Server.Interfaces
{
    public record TokenResult(string Token, DateTime Expires);

    public interface ITokenService
    {
        TokenResult CreateToken(User user);
    }
}
