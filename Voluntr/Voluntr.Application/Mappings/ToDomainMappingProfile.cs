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

            CreateMap<AddressViewModel, AddressCommand>();
            CreateMap<AddressCommand, Address>();
            CreateMap<UploadPictureRequestViewModel, UploadPictureCommand>();

            #endregion

            #region Project

            CreateMap<ProjectRequestViewModel, AddProjectCommand>();
            CreateMap<ProjectRequestViewModel, UpdateProjectCommand>();

            #region Quest

            CreateMap<QuestRequestViewModel, AddQuestCommand>();
            CreateMap<QuestRequestViewModel, UpdateQuestCommand>();

            #endregion

            #endregion

            #region Volunteer

            CreateMap<OnboardingTask, OnboardingTaskDto>();
            CreateMap<VolunteerRequestViewModel, VolunteerCommand>();

            #endregion

            #region Ngo

            #endregion

            #region Company

            #endregion

            #region Authentication

            CreateMap<RegisterUserViewModel, RegisterUserCommand>();
            CreateMap<VolunteerRegisterViewModel, VolunteerRegisterCommand>();
            CreateMap<NgoRegisterViewModel, NgoRegisterCommand>();
            CreateMap<CompanyRegisterViewModel, CompanyRegisterCommand>();
            CreateMap<AuthenticationRequestViewModel, LoginUserCommand>();
            CreateMap<OAuthAuthenticationRequestViewModel, OAuthLoginUserCommand>();
            CreateMap<ResetPasswordRequestViewModel, ResetPasswordRequestCommand>();
            CreateMap<ResetPasswordViewModel, ResetPasswordCommand>();
            CreateMap<UpdatePasswordViewModel, UpdatePasswordCommand>();
            CreateMap<VerifyAccountViewModel, VerifyAccountCommand>();
            CreateMap<CompanyRegisterCommand, CompanyRegisterViewModel>();
            CreateMap<NgoRegisterCommand, NgoRegisterViewModel>();
            CreateMap<VolunteerRegisterCommand, VolunteerRegisterViewModel>();

            #endregion
        }
    }
}
