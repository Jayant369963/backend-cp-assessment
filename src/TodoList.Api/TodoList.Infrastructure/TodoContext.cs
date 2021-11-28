using Microsoft.EntityFrameworkCore;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Infrastructure.Data.Config;

namespace TodoList.Infrastructure;

public class TodoContext : DbContext, ITodoContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TodoItemEntityTypeConfiguration());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
