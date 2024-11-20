﻿using Voluntr.Crosscutting.Infrastructure.Repositories;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Models;
using Voluntr.Infrastructure.Contexts;

namespace Voluntr.Infrastructure.Repositories
{
    public class ResetPasswordTryRepository(SqlContext context)
        : SqlRepository<ResetPasswordTry>(context), IResetPasswordTryRepository;
}
