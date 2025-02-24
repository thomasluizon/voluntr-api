﻿using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class ResetPasswordCommand : AccountCommand<AuthenticationDto>
    {
        public override bool IsValid()
        {
            ValidationResult = new ResetPasswordValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
