using AutoMapper;
using Voluntr.Application.Mappings;

namespace Voluntr.Api.Configurations
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            var config = new MapperConfiguration(x =>
            {
                x.AddProfile(new ToViewModelMappingProfile());
                x.AddProfile(new ToDomainMappingProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
