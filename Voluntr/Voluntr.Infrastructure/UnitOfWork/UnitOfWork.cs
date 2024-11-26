using Microsoft.EntityFrameworkCore;
using Voluntr.Domain.Interfaces.UnitOfWork;
using Voluntr.Infrastructure.Contexts;

namespace Voluntr.Infrastructure.UnitOfWork
{
    public class UnitOfWork(SqlContext context) : IUnitOfWork
    {
        public async Task<bool> CommitAsync()
        {
            var executionStrategy = context.Database.CreateExecutionStrategy();

            return await executionStrategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();
                try
                {
                    bool success = (await context.SaveChangesAsync()) > 0;

                    if (success)
                    {
                        await transaction.CommitAsync();
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                    }

                    return success;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
