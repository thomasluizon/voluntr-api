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

            CreateMap<RegisterUserViewModel, RegisterUserCommand>();
            CreateMap<AuthenticationRequestViewModel, LoginUserCommand>();

            #endregion
        }
    }
}
