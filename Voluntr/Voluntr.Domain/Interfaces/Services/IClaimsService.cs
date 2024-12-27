using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.Interfaces.Services
{
    public interface IClaimsService
    {
        Guid? GetCurrentUserId();
        string GenerateAuthToken(User user, UserTypeEnum userType);
        string GenerateGenericToken(User user, int expiryMinutes = 15);
        bool IsTokenValid(string token);
        Guid? GetUserIdFromToken(string token);
    }
}
