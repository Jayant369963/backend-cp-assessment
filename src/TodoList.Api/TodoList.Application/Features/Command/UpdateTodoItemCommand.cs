using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Application.Interfaces;

namespace TodoList.Application.Command;

public class UpdateTodoItemCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, bool>
    {
        private readonly ITodoContext _todoContext;

        public UpdateTodoItemCommandHandler(ITodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public async Task<bool> Handle(UpdateTodoItemCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var todoItem = await _todoContext.TodoItems.Where(a => a.Id == command.Id).SingleOrDefaultAsync(cancellationToken);

                if (todoItem == null)
                {
                    return false;
                }
                else
                {
                    todoItem.Id = command.Id;
                    todoItem.Description = command.Description;
                    todoItem.IsCompleted = command.IsCompleted;
                    await _todoContext.SaveChangesAsync(cancellationToken);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
