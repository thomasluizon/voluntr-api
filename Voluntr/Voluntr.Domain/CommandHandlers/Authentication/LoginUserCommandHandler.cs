﻿using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.Interfaces.Services;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;

namespace Voluntr.Domain.CommandHandlers
{
    public class LoginUserCommandHandler(
        IMediatorHandler mediator,
        IUserRepository userRepository,
        ICryptographyService cryptographyService,
        IClaimsService claimsService
    ) : MediatorResponseCommandHandler<LoginUserCommand, AuthenticationDto>(mediator)
    {
        public override async Task<AuthenticationDto> AfterValidation(LoginUserCommand request)
        {
            var user = await userRepository.GetFirstByExpressionAsync(x => x.Email == request.Email.Trim());

            if (
                user == null ||
                string.IsNullOrEmpty(user.Password) ||
                cryptographyService.Decrypt(user.Password) != request.Password.Trim()
            )
            {
                NotifyError("Credenciais inválidas");
                return null;
            }

            return new AuthenticationDto
            {
                AccessToken = claimsService.GenerateToken(user),
            };
        }
    }
}
