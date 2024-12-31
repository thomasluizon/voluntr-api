using AutoMapper;
using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.Interfaces.Services;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Events;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Interfaces.UnitOfWork;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.CommandHandlers
{
    public class RegisterUserCommandHandler(
        IMediatorHandler mediator,
        IUserRepository userRepository,
        ICryptographyService cryptographyService,
        IClaimsService claimsService,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IVolunteerRepository volunteerRepository,
        IAddressRepository addressRepository,
        INgoRepository ngoRepository,
        ICompanyRepository companyRepository
    ) : MediatorResponseCommandHandler<RegisterUserCommand, CommandResponseDto>(mediator)
    {
        public override async Task<CommandResponseDto> AfterValidation(RegisterUserCommand request)
        {
            var userExists = await userRepository.ExistsByExpressionAsync(
                x => x.Email == request.Email.Trim()
            );

            if (userExists)
            {
                NotifyError("Já existe um usuário com o email informado");
                return null;
            }

            var user = new User
            {
                Email = request.Email.Trim(),
                Name = request.Name.Trim(),
                Password = cryptographyService.Encrypt(request.Password.Trim()),
                Phone = new string(request.Phone.Where(char.IsDigit).ToArray()),
            };

            var address = mapper.Map<Address>(request.Address);
            address.UserId = user.Id;

            await addressRepository.InsertAsync(address);
            await userRepository.InsertAsync(user);

            if (request.UserType == UserTypeEnum.Volunteer.GetDescription())
            {
                var volunteer = new Volunteer
                {
                    BirthDate = request.VolunteerRegister.BirthDate,
                    Surname = request.VolunteerRegister.Surname,
                    UserId = user.Id
                };

                await volunteerRepository.InsertAsync(volunteer);
            }
            else if (request.UserType == UserTypeEnum.Ngo.GetDescription())
            {
                var ngo = new Ngo
                {
                    UserId = user.Id,
                };

                await ngoRepository.InsertAsync(ngo);
            }
            else if (request.UserType == UserTypeEnum.Company.GetDescription())
            {
                var company = new Company
                {
                    UserId = user.Id,
                };

                await companyRepository.InsertAsync(company);
            }

            if (!HasNotification() && await unitOfWork.CommitAsync())
            {
                request.ExecutedSuccessfullyCommand = true;

                var emailActivationToken = claimsService.GenerateGenericToken(user);

                await mediator.PublishEvent(new EmailActivationEvent
                {
                    EmailActivationToken = emailActivationToken,
                    User = user
                });

                return new CommandResponseDto { Id = user.Id };
            }
            else
                NotifyError(Values.Message.DefaultError);

            return null;
        }
    }
}