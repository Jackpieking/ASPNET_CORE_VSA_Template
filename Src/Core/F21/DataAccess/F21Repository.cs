using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FA1.DbContext;
using FA1.Entities;
using Microsoft.EntityFrameworkCore;

namespace F21.DataAccess;

public sealed class F21Repository : IF21Repository
{
    private readonly AppDbContext _appContext;

    public F21Repository(AppDbContext context)
    {
        _appContext = context;
    }

    public async Task<bool> ChangeDueDateAsync(long taskId, DateTime dueDate, CancellationToken ct)
    {
        var dbResult = true;

        await _appContext
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(async () =>
            {
                await using var dbTransaction = await _appContext.Database.BeginTransactionAsync(
                    IsolationLevel.ReadCommitted,
                    ct
                );

                try
                {
                    await _appContext
                        .Set<TodoTaskEntity>()
                        .Where(task => task.Id == taskId)
                        .ExecuteUpdateAsync(
                            setProp => setProp.SetProperty(entity => entity.DueDate, dueDate),
                            ct
                        );

                    await dbTransaction.CommitAsync(ct);
                }
                catch (DbUpdateException)
                {
                    await dbTransaction.RollbackAsync(ct);

                    dbResult = false;
                }
            });

        return dbResult;
    }

    public Task<bool> DoesTodoTaskExistAsync(long taskId, CancellationToken ct)
    {
        return _appContext.Set<TodoTaskEntity>().AnyAsync(entity => entity.Id == taskId, ct);
    }
}
