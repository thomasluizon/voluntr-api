using System.Security.Claims;
using Voluntr.Crosscutting.Domain.Events.Notifications;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.Helpers.Validators;
using Voluntr.Crosscutting.Domain.Interfaces.Services;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.Services
{
    public class ClaimsService(
       ICryptographyService cryptographyService,
       ClaimsPrincipal claims,
       IMediatorHandler mediator
    ) : IClaimsService
    {
        public Guid? GetCurrentUserId()
        {
            var userId = claims.GetUserIdFromToken();

            if (string.IsNullOrEmpty(userId))
                return null;

            userId = cryptographyService.Decrypt(userId);

            if (!Validator.IsGuid(userId))
            {
                NotifyError(Values.Message.UserRequestNotFound);
                return null;
            }

            return Guid.Parse(userId);
        }

        public string GenerateToken(User user)
        {
            throw new NotImplementedException();
        }

        protected void NotifyError(string message) => NotifyError(string.Empty, message);

        protected void NotifyError(string code, string message)
        {
            mediator.PublishEvent(new DomainNotification(code, message));
        }
    }
}
