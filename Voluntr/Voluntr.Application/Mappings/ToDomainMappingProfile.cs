using AutoMapper;
using Voluntr.Application.ViewModels;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Models;

namespace Voluntr.Application.Mappings
{
    public class ToDomainMappingProfile : Profile
    {
        public ToDomainMappingProfile()
        {
            #region User

            CreateMap<Volunteer, VolunteerDto>();

            #endregion

            #region Authentication

            CreateMap<RegisterUserViewModel, RegisterUserCommand>();
            CreateMap<AuthenticationRequestViewModel, LoginUserCommand>();
            CreateMap<OAuthAuthenticationRequestViewModel, OAuthLoginUserCommand>();
            CreateMap<ResetPasswordRequestViewModel, ResetPasswordRequestCommand>();
            CreateMap<ResetPasswordViewModel, ResetPasswordCommand>();

            #endregion
        }
    }
}
