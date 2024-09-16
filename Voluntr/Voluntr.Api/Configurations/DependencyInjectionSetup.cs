using Voluntr.IoC;

namespace Voluntr.Api.Configurations
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
