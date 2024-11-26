using FluentValidation;
using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class UserValidation<TCommand> : AbstractValidator<TCommand> where TCommand : UserCommand
    {
    }
}
