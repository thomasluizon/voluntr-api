﻿using Voluntr.Crosscutting.Domain.Interfaces.Repositories;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.Interfaces.Repositories
{
    public interface IProjectRepository : ISqlRepository<Project>;
}
