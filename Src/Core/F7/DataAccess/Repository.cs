using System.Threading;
using System.Threading.Tasks;
using F7.Models;
using FA1.DbContext;
using FA1.Entities;
using Microsoft.EntityFrameworkCore;

namespace F7.DataAccess;

public sealed partial class Repository : IRepository
{
    private readonly AppDbContext _appContext;

    public Repository(AppDbContext context)
    {
        _appContext = context;
    }

    public async Task<bool> CreateTaskTodoListAsync(
        TaskTodoListModel taskTodoList,
        CancellationToken ct
    )
    {
        var taskTodoListEntity = new TodoTaskListEntity
        {
            Id = taskTodoList.Id,
            Name = taskTodoList.Name,
            CreatedDate = taskTodoList.CreatedDate,
            UserId = taskTodoList.UserId,
        };

        try
        {
            await _appContext.Set<TodoTaskListEntity>().AddAsync(taskTodoListEntity, ct);

            await _appContext.SaveChangesAsync(ct);

            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }
}
