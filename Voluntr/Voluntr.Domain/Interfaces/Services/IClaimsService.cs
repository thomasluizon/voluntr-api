using Voluntr.Domain.Models;

namespace Voluntr.Domain.Interfaces.Services
{
    public interface IClaimsService
    {
        Guid? GetCurrentUserId();
        string GenerateAuthToken(User user);
        string GenerateGenericToken(User user, int expiryMinutes = 15);
        bool IsTokenValid(string token);
        Guid? GetUserIdFromToken(string token);
    }
}
