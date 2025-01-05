using AutoMapper;
using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Interfaces.UnitOfWork;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.CommandHandlers
{
    public class UpdateVolunteerCommandHandler(
        IMediatorHandler mediator,
        IClaimsService claimsService,
        IVolunteerRepository volunteerRepository,
        IMapper mapper,
        IAddressRepository addressRepository,
        IUnitOfWork unitOfWork
    ) : MediatorCommandHandler<UpdateVolunteerCommand>(mediator)
    {
        public override async Task AfterValidation(UpdateVolunteerCommand request)
        {
            var userId = claimsService.GetCurrentUserId();

            var volunteer = await volunteerRepository.GetFirstByExpressionAsync(
                x => x.UserId == userId,
                x => x.User
            );

            if (volunteer == null)
            {
                NotifyError("O voluntário informado não foi encontrado");
                return;
            }

            if (await volunteerRepository.ExistsByExpressionAsync(
                x => x.Nickname == request.Nickname &&
                x.Id != volunteer.Id
            ))
            {
                NotifyError("Já existe um voluntário com o apelido informado");
                return;
            }

            if (await volunteerRepository.ExistsByExpressionAsync(
                x => x.User.Email == request.Email &&
                x.Id != volunteer.Id
            ))
            {
                NotifyError("Já existe um voluntário com o email informado");
                return;
            }

            volunteer.Nickname = request.Nickname;
            volunteer.User.Name = request.Name;
            volunteer.Surname = request.Surname;
            volunteer.User.Email = request.Email;
            volunteer.User.Phone = request.Phone;

            var address = mapper.Map<Address>(request.Address);
            address.UserId = volunteer.UserId;

            await addressRepository.InsertAsync(address);
            await volunteerRepository.UpdateAsync(volunteer);

            if (!HasNotification() && await unitOfWork.CommitAsync())
                request.ExecutedSuccessfullyCommand = true;
            else
                NotifyError(Values.Message.DefaultError);
        }
    }
}
