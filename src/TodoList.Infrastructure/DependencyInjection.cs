using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.Interfaces;
using TodoList.Infrastructure;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoItemsDB"));

        services.AddScoped<ITodoContext>(provider => provider.GetService<TodoContext>());
    }
}