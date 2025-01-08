using FluentValidation;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Domain.Commands;
using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Validations.User;

namespace Voluntr.Domain.Validations
{
    public class AccountValidation<TCommand, TResponse>
        : AbstractValidator<TCommand> where TCommand : AccountCommand<TResponse>
    {
        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotNull().NotEmpty().WithMessage("O email é obrigatório")
                .EmailAddress().WithMessage("O email informado não é valido")
                .MaximumLength(100).WithMessage("O email deve conter no máximo 100 caracteres");
        }

        protected void ValidatePassword()
        {
            RuleFor(c => c.Password)
                .NotNull().NotEmpty().WithMessage("A senha é obrigatória")
                .MinimumLength(6).WithMessage("A senha deve conter no mínimo 6 caracteres")
                .MaximumLength(100).WithMessage("A senha deve conter no máximo 100 caracteres");
        }

        protected void ValidateNewPassword()
        {
            RuleFor(c => c.NewPassword)
                .NotNull().NotEmpty().WithMessage("A nova senha é obrigatória")
                .MinimumLength(6).WithMessage("A nova senha deve conter no mínimo 6 caracteres")
                .MaximumLength(100).WithMessage("A nova senha deve conter no máximo 100 caracteres");
        }

        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotNull().NotEmpty().WithMessage("O nome do usuário é obrigatório")
                .MaximumLength(100).WithMessage("O nome do usuário deve conter no máximo 100 caracteres");
        }

        protected void ValidateToken()
        {
            RuleFor(c => c.Token)
                .NotNull().NotEmpty().WithMessage("O token é obrigatório");
        }

        protected void ValidateRegister()
        {
            RuleFor(x => x.UserType)
                .NotNull().NotEmpty().WithMessage("O tipo de usuário é obrigatório")
                .Must(ValidateUserType).WithMessage("O tipo de usuário informado é inválido");

            RuleFor(x => x.Phone)
                .Must(ValidatePhoneNumber).WithMessage("O telefone informado é inválido. Deve conter exatamente 11 números.");

            #region Volunteer

            RuleFor(x => x.VolunteerRegister)
                .NotNull().When(x => x.UserType.Trim() == UserTypeEnum.Volunteer.GetDescription())
                .WithMessage("As informações do voluntário são obrigatórias para o tipo de usuário Voluntário.");

            When(x => x.UserType.Trim() == UserTypeEnum.Volunteer.GetDescription(), () =>
            {
                RuleFor(x => x.VolunteerRegister.Surname)
                    .NotEmpty().WithMessage("O sobrenome do voluntário é obrigatório.")
                    .MaximumLength(100).WithMessage("O sobrenome do voluntário deve ter no máximo 100 caractéres");

                RuleFor(x => x.VolunteerRegister.BirthDate)
                    .NotNull().WithMessage("A data de nascimento do voluntário é obrigatória.")
                    .LessThan(DateTime.Now.ToBrazilianTimezone()).WithMessage("A data de nascimento do voluntário deve ser no passado.");
            });

            #endregion

            #region Ngo

            RuleFor(x => x.NgoRegister)
                .NotNull().When(x => x.UserType.Trim() == UserTypeEnum.Ngo.GetDescription())
                .WithMessage("As informações da ONG são obrigatórias para o tipo de usuário ONG.");

            #endregion

            #region Company

            RuleFor(x => x.CompanyRegister)
                .NotNull().When(x => x.UserType.Trim() == UserTypeEnum.Company.GetDescription())
                .WithMessage("As informações da empresa são obrigatórias para o tipo de usuário Empresa.");

            #endregion
        }

        protected void ValidateAddress()
        {
            RuleFor(x => x.Address)
                .NotNull().WithMessage("O endereço é obrigatório.")
                .SetValidator(new AddressValidator());
        }

        private bool ValidatePhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return true;

            var numericPhone = new string(phone.Where(char.IsDigit).ToArray());

            return numericPhone.Length == 11;
        }

        private bool ValidateUserType(string userType)
        {
            return userType.Trim() == UserTypeEnum.Volunteer.GetDescription()
                || userType.Trim() == UserTypeEnum.Ngo.GetDescription()
                || userType.Trim() == UserTypeEnum.Company.GetDescription();
        }

        #region OAuth

        protected void ValidateOAuthToken()
        {
            RuleFor(c => c.OAuthToken)
                .NotNull().NotEmpty().WithMessage("O token é obrigatório");
        }

        protected void ValidateOAuthProvider()
        {
            RuleFor(c => c.OAuthProviderName)
                .NotNull().NotEmpty().WithMessage("O provedor de autenticação é obrigatório")
                .Must(ValidateOAuthProvider).WithMessage("Não é possível fazer login com o provedor de autenticação informado");
        }

        private bool ValidateOAuthProvider(string provider)
        {
            return provider.Trim() == OAuthProviderNameEnum.Google.GetDescription();
        }

        #endregion
    }
}