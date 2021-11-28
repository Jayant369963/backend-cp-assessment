using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;

namespace TodoList.Application.Features.Query;

public class GetTodoItemByIdQuery : IRequest<TodoItem>
{
    public Guid Id { get; set; }

    public class GetTodoItemByIdHandler : IRequestHandler<GetTodoItemByIdQuery, TodoItem>
    {
        private readonly ITodoContext _todoContext;

        public GetTodoItemByIdHandler(ITodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public async Task<TodoItem> Handle(GetTodoItemByIdQuery query, CancellationToken cancellationToken)
        {
            return await _todoContext.TodoItems.SingleOrDefaultAsync(t => t.Id == query.Id && (!t.IsCompleted), cancellationToken);
        }
    }
}
