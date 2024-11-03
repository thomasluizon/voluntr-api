using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.Interfaces.Services
{
    public interface IOAuthService
    {
        Task<OAuthUserDto> ValidateOAuthTokenAsync(string token, OAuthProvider OAuthProvider);
    }
}
