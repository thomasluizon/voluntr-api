using System.Security.Claims;
using Voluntr.Crosscutting.Domain.Events.Notifications;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.Helpers.Validators;
using Voluntr.Crosscutting.Domain.Interfaces.Services;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Services;

namespace Voluntr.Domain.Services
{
    public class ClaimsService(
       ICryptographyService cryptographyService,
       ClaimsPrincipal claims,
       IMediatorHandler mediator
    ) : IClaimsService
    {
        public Guid GetCurrentUserId()
        {
            string userId = claims.GetUserIdFromToken();

            if (!string.IsNullOrEmpty(userId))
            {
                userId = cryptographyService.Decrypt(userId);

                if (!Validator.IsGuid(userId))
                {
                    NotifyError(Values.Message.UserRequestNotFound);
                    return Guid.Empty;
                }
            }

            return Guid.Parse(userId);
        }

        public string GetCurrentUserRole()
        {
            var role = claims.GetRolesFromToken();

            if (string.IsNullOrEmpty(role))
            {
                NotifyError(Values.Message.UserRequestNotFound);
                return null;
            }

            return cryptographyService.Decrypt(role);
        }

        public bool IsAdmin()
        {
            var roles = GetCurrentUserRole();

            return roles.Contains("Admin");
        }

        protected void NotifyError(string message) => NotifyError(string.Empty, message);

        protected void NotifyError(string code, string message)
        {
            mediator.PublishEvent(new DomainNotification(code, message));
        }
    }
}
