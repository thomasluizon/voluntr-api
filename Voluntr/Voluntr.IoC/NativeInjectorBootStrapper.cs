

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Voluntr.Crosscutting.Domain.Events.Notifications;
using Voluntr.Crosscutting.Domain.Interfaces.Services;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Crosscutting.Domain.Services.Criptography;
using Voluntr.Crosscutting.Domain.Services.Storage;

namespace Voluntr.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Crosscutting injections

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<ICryptographyService, CryptographyService>();
            services.AddScoped<IStorageService, AzureStorageService>();

            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Scan(s => s
               .FromApplicationDependencies(a => a.FullName.StartsWith("Voluntr"))
               .AddClasses().AsMatchingInterface((service, filter) =>
                   filter.Where(i => i.Name.Equals($"I{service.Name}", StringComparison.OrdinalIgnoreCase))).WithScopedLifetime()
               .AddClasses(x => x.AssignableTo(typeof(IMediator))).AsImplementedInterfaces().WithScopedLifetime()
               .AddClasses(x => x.AssignableTo(typeof(IRequestHandler<>))).AsImplementedInterfaces().WithScopedLifetime()
               .AddClasses(x => x.AssignableTo(typeof(IRequestHandler<,>))).AsImplementedInterfaces().WithScopedLifetime()
               .AddClasses(x => x.AssignableTo(typeof(INotificationHandler<>))).AsImplementedInterfaces().WithScopedLifetime()
            );

            services.AddTransient(s => s.GetService<IHttpContextAccessor>().HttpContext.User);
        }
    }
}
