using AutoMapper;
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
        }
    }
}
