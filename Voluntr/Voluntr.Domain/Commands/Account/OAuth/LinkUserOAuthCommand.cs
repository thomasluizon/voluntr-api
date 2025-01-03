﻿using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class LinkUserOAuthCommand : AccountCommand<AuthenticationDto>
    {
        public override bool IsValid()
        {
            ValidationResult = new LinkUserOAuthValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
