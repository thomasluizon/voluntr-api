using AutoMapper;
using Voluntr.Application.ViewModels;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Application.Mappings
{
    public class ToViewModelMappingProfile : Profile
    {
        public ToViewModelMappingProfile()
        {
            CreateMap<VolunteerDto, VolunteerResponseViewModel>();
        }
    }
}
