using Voluntr.Crosscutting.Infrastructure.Repositories;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Models;
using Voluntr.Infrastructure.Contexts;

namespace Voluntr.Infrastructure.Repositories
{
    public class AchievementCategoryRepository(SqlContext context) : SqlRepository<AchievementCategory>(context), IAchievementCategoryRepository;
}
