using Voluntr.Domain.Models;

namespace Voluntr.Domain.Interfaces.Services
{
    public interface IClaimsService
    {
        Guid? GetCurrentUserId();
        string GenerateToken(User user);
        string GenerateResetToken(User user, int expiryMinutes = 15);
        bool IsTokenValid(string token);
    }
}
