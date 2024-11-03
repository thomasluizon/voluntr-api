using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Interfaces.Services
{
    public interface IOAuthService
    {
        Task<GoogleOAuthUserDto> ValidateGoogleTokenAsync(string token);
    }
}
