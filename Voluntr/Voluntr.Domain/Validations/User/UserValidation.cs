using FluentValidation;
using Microsoft.AspNetCore.Http;
using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class UserValidation<TCommand> : AbstractValidator<TCommand> where TCommand : UserCommand
    {
        protected void ValidatePicture()
        {
            RuleFor(x => x.Picture)
                .NotNull().WithMessage("O arquivo da imagem é obrigatório.")
                .Must(x => x.Length > 0).WithMessage("O arquivo enviado está vazio.")
                .Must(IsValidExtension).WithMessage("Apenas arquivos .png, .jpg ou .jpeg são suportados.")
                .Must(IsValidSize).WithMessage("O arquivo enviado excede o tamanho máximo permitido de 32MB.");
        }

        private bool IsValidExtension(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName)?.ToLower();
            return extension == ".png" || extension == ".jpg" || extension == ".jpeg";
        }

        private bool IsValidSize(IFormFile file)
        {
            const long maxSizeInBytes = 32 * 1024 * 1024;
            return file.Length <= maxSizeInBytes;
        }
    }
}
