using Voluntr.Domain.Interfaces.UnitOfWork;
using Voluntr.Infrastructure.Contexts;

namespace Voluntr.Infrastructure.UnitOfWork
{
    public class UnitOfWork(SqlContext context) : IUnitOfWork
    {
        public async Task<bool> CommitAsync()
        {
            using var transaction = context.Database.BeginTransaction();
            bool success = (await context.SaveChangesAsync()) > 0;

            if (success)
                await transaction.CommitAsync();
            else
                await transaction.RollbackAsync();

            return success;
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
