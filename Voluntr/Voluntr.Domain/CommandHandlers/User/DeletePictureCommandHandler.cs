using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.Interfaces.Services;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Interfaces.UnitOfWork;

namespace Voluntr.Domain.CommandHandlers
{
    public class DeletePictureCommandHandler(
        IMediatorHandler mediator,
        IClaimsService claimsService,
        IStorageService storageService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork
    ) : MediatorCommandHandler<DeletePictureCommand>(mediator)
    {
        public override async Task AfterValidation(DeletePictureCommand request)
        {
            var userId = claimsService.GetCurrentUserId();

            var user = await userRepository.GetByIdAsync(userId.Value);

            if (user == null)
            {
                NotifyError("O usuário informado não foi encontrado");
                return;
            }

            var container = Values.BlobPath.ImagesContainer;
            var path = string.Format(Values.BlobPath.UserPictures, userId);

            await storageService.DeleteAllFiles(container, path);

            user.Picture = null;
            await userRepository.UpdateAsync(user);

            if (!HasNotification() && await unitOfWork.CommitAsync())
                request.ExecutedSuccessfullyCommand = true;
            else
                NotifyError(Values.Message.DefaultError);
        }
    }
}
