using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Data.Config;

public class TodoItemEntityTypeConfiguration
: IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> todoItemConfiguration)
    {
        todoItemConfiguration.ToTable(nameof(TodoItem));

        todoItemConfiguration.HasKey(b => b.Id);

        todoItemConfiguration.Property(b => b.Id).IsRequired();

        todoItemConfiguration.Property(b => b.Description).IsRequired();

        todoItemConfiguration.HasIndex(b => b.Description).IsUnique(true);
        
        todoItemConfiguration.Property(b => b.IsCompleted);
    }
}
