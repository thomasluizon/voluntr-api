﻿using AutoMapper;
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
            CreateMap<AddressViewModel, AddressCommand>();
            CreateMap<AddressCommand, Address>();
            CreateMap<CompanyRegisterCommand, CompanyRegisterViewModel>();
            CreateMap<NgoRegisterCommand, NgoRegisterViewModel>();
            CreateMap<VolunteerRegisterCommand, VolunteerRegisterViewModel>();

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

            #endregion
        }
    }
}
