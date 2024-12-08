using AutoMapper;
using Voluntr.Application.ViewModels;
using Voluntr.Crosscutting.Domain.Helpers.Pagination;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Application.Mappings
{
    public class ToViewModelMappingProfile : Profile
    {
        public ToViewModelMappingProfile()
        {
            #region Domain

            CreateMap<CommandResponseDto, CommandResponseViewModel>();
            CreateMap<AutocompleteDto, AutocompleteViewModel>();
            CreateMap<PagedResult<AutocompleteDto>, PagedResult<AutocompleteViewModel>>();

            #endregion

            #region User

            CreateMap<VolunteerDto, VolunteerResponseViewModel>();

            #endregion

            #region Authentication

            CreateMap<AuthenticationDto, AuthenticationResponseViewModel>();

            #endregion

            #region External

            CreateMap<ZipCodeInformationDto, ZipCodeInformationViewModel>();

            #endregion
        }
    }
}
