using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Commands
{
    public class HandleGoogleCallbackCommand : AuthenticationCommand<AuthenticationDto>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
