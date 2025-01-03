using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Voluntr.Api.Attributes;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Interfaces.Services;

namespace Voluntr.Api.Filters
{
    public class UserTypeValidationFilter(IClaimsService claimsService) : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var endpointAttributes = context.ActionDescriptor.EndpointMetadata;
            var controllerAttributes = context.Controller.GetType().GetCustomAttributes(true);

            var userType = claimsService.GetCurrentUserType();

            if (HasAttribute<VolunteerAttribute>(endpointAttributes, controllerAttributes) && userType != UserTypeEnum.Volunteer.GetDescription())
            {
                context.Result = new UnauthorizedObjectResult("O usuário informado não é um voluntário");
                return;
            }

            if (HasAttribute<NgoAttribute>(endpointAttributes, controllerAttributes) && userType != UserTypeEnum.Ngo.GetDescription())
            {
                context.Result = new UnauthorizedObjectResult("O usuário informado não é uma ONG");
                return;
            }

            if (HasAttribute<CompanyAttribute>(endpointAttributes, controllerAttributes) && userType != UserTypeEnum.Company.GetDescription())
            {
                context.Result = new UnauthorizedObjectResult("O usuário informado não é uma empresa");
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        private static bool HasAttribute<T>(IEnumerable<object> endpointAttributes, IEnumerable<object> controllerAttributes)
        {
            return endpointAttributes.OfType<T>().Any() || controllerAttributes.OfType<T>().Any();
        }
    }
}
