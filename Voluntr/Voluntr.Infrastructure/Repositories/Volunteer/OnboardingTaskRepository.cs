﻿using Voluntr.Crosscutting.Infrastructure.Repositories;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Models;
using Voluntr.Infrastructure.Contexts;

namespace Voluntr.Infrastructure.Repositories
{
    public class OnboardingTaskRepository(SqlContext context) : SqlRepository<OnboardingTask>(context), IOnboardingTaskRepository;
}
