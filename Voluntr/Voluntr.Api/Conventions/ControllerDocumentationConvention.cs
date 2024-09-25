using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Voluntr.Api.Conventions
{
    public class ControllerDocumentationConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller == null)
                return;

            var lowercaseControllerName = controller.ControllerName.ToLowerInvariant();

            foreach (var selector in controller.Selectors)
            {
                if (selector.AttributeRouteModel != null)
                {
                    var template = selector.AttributeRouteModel.Template;

                    if (!string.IsNullOrEmpty(template) && template.Contains("[controller]"))
                    {
                        selector.AttributeRouteModel.Template = template.Replace("[controller]", lowercaseControllerName);
                    }
                }
            }
        }
    }
}
