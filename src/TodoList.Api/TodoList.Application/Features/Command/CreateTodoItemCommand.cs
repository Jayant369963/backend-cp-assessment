using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;

namespace TodoList.Application.Command;

public class CreateTodoItemCommand : IRequest<TodoItem>
{
    public string Description { get; set; }
    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItem>
    {
        private readonly ITodoContext _context;
        public CreateTodoItemCommandHandler(ITodoContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> Handle(CreateTodoItemCommand command, CancellationToken cancellationToken)
        {
            if (await TodoDescriptionAlreadyExistsAsync(command.Description, cancellationToken))
            {
                throw new InvalidOperationException("Description already exists");
            }

            var todoItem = new TodoItem
            {
                Id = new Guid(),
                Description = command.Description,
                IsCompleted = false
            };
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync(cancellationToken);
            return todoItem;
        }

        private async Task<bool> TodoDescriptionAlreadyExistsAsync(string description, CancellationToken cancellationToken)
        {
            return await _context.TodoItems.AnyAsync(t => t.Description == description && !t.IsCompleted, cancellationToken);
        }
    }
}