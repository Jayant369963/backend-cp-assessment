using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces
{
    public interface ITodoContext
    {
        DbSet<TodoItem> TodoItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
