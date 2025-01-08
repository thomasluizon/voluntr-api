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
    public class UploadPictureCommandHandler(
        IMediatorHandler mediator,
        IUserRepository userRepository,
        IClaimsService claimsService,
        IStorageService storageService,
        IUnitOfWork unitOfWork
    ) : MediatorCommandHandler<UploadPictureCommand>(mediator)
    {
        public override async Task AfterValidation(UploadPictureCommand request)
        {
            var userId = claimsService.GetCurrentUserId();

            var user = await userRepository.GetByIdAsync(userId.Value);

            if (user == null)
            {
                NotifyError("O usuário informado não foi encontrado");
                return;
            }

            var extension = Path.GetExtension(request.Picture.FileName)?.ToLower();

            var container = Values.BlobPath.ImagesContainer;
            var path = string.Format(Values.BlobPath.UserPictures, userId);
            var fileName = $"picture{extension}";

            await storageService.DeleteAllFiles(container, path);

            using var memoryStream = new MemoryStream();
            await request.Picture.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            var pictureUrl = await storageService.UploadFile(container, path, fileName, fileBytes);

            user.Picture = pictureUrl;
            await userRepository.UpdateAsync(user);

            if (!HasNotification() && await unitOfWork.CommitAsync())
                request.ExecutedSuccessfullyCommand = true;
            else
                NotifyError(Values.Message.DefaultError);
        }
    }
}
