using Microsoft.AspNetCore.Mvc;
using Voluntr.Crosscutting.Domain.Events.Notifications;
using Voluntr.Crosscutting.Domain.Helpers.Validators;
using Voluntr.Crosscutting.Domain.MediatR;

namespace Voluntr.Crosscutting.Domain.Controller
{
    public abstract class ApiController(IMediatorHandler mediator) : ControllerBase
    {
        private readonly DomainNotificationHandler notifications = (DomainNotificationHandler)mediator.GetNotificationHandler();
        protected IEnumerable<DomainNotification> Notifications => notifications.GetNotifications();

        protected bool IsValidOperation()
        {
            return !notifications.HasNotifications();
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
                return Ok(result);

            if (notifications.GetNotifications().Where(x => x.Key == "403").Any())
                return Forbid();

            return BadRequest(new BadRequestResponse(notifications.GetNotifications().Select(n => n.Value)));
        }

        protected IActionResult ModalStateResponse()
        {
            NotifyModelStateErrors();

            return Response();
        }

        protected IActionResult ResponseWithError(string error)
        {
            NotifyError(error);

            return Response();
        }

        protected void NotifyModelStateErrors()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                var erroMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected IEnumerable<string> GetNotificationErrors()
        {
            return notifications.GetNotifications().Select(t => t.Value);
        }

        protected void NotifyError(string code, string message)
        {
            mediator.PublishEvent(new DomainNotification(code, message));
        }

        protected void NotifyError(string message) => NotifyError(string.Empty, message);

        protected bool IsNullRequest(object request)
        {
            if (request != null) return false;

            NotifyError("O objeto informado é inválido. Verifique os parâmetros passados e tente novamente.");

            return true;
        }

        protected bool ValidateStringToGuidParams(string guid)
        {
            bool isValid = Validator.IsGuid(guid);

            if (!isValid)
                NotifyError("O parâmetro informado não é válido, por favor informe um valor de padrão UUID.");

            return isValid;
        }
    }

    public class BadRequestResponse(IEnumerable<string> errors)
    {
        public IEnumerable<string> Errors { get; } = errors;
    }

    public class UnauthorizedResponse();
}
