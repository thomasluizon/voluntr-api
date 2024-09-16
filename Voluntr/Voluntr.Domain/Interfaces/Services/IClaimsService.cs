namespace Voluntr.Domain.Interfaces.Services
{
    public interface IClaimsService
    {
        Guid GetCurrentUserId();
        string GetCurrentUserRole();
        bool IsAdmin();
    }
}
