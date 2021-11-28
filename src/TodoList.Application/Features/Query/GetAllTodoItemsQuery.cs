using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;

namespace TodoList.Application.Query;

public class GetAllTodoItemsQuery : IRequest<IEnumerable<TodoItem>>
{
    public class GetAllTodoItemsQueryHandler : IRequestHandler<GetAllTodoItemsQuery, IEnumerable<TodoItem>>
    {
        private readonly ITodoContext _context;
        public GetAllTodoItemsQueryHandler(ITodoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TodoItem>> Handle(GetAllTodoItemsQuery query, CancellationToken cancellationToken)
        {
            var todoItems = await _context.TodoItems.Where(t => !t.IsCompleted).ToListAsync(cancellationToken);
            if (todoItems == null)
            {
                return null;
            }
            return todoItems.AsReadOnly();
        }
    }
}
