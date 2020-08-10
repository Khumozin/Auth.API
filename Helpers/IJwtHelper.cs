using Auth.API.Models;

namespace Auth.API.Helpers
{
    public interface IJwtHelper
    {
        string GenerateToken(User user);
    }
}