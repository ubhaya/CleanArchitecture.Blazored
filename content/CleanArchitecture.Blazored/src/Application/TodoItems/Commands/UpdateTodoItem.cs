using CleanArchitecture.Blazored.Application.Common.Services.Data;
using CleanArchitecture.Blazored.Domain.Enums;
using CleanArchitecture.Blazored.Domain.Events;
using CleanArchitecture.Blazored.WebUi.Shared.TodoItems;

namespace CleanArchitecture.Blazored.Application.TodoItems.Commands;

public sealed record UpdateTodoItemCommand(UpdateTodoItemRequest Item) : IRequest<Unit>;

public sealed class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems.FirstOrDefaultAsync(
            i => i.Id == request.Item.Id, cancellationToken);

        Guard.Against.NotFound(request.Item.Id, entity);

        entity!.ListId = request.Item.ListId;
        entity.Title = request.Item.Title;
        entity.Done = request.Item.Done;
        entity.Priority = (PriorityLevel)request.Item.Priority;
        entity.Note = request.Item.Note;

        if (entity.Done)
        {
            entity.AddDomainEvent(new TodoItemCompletedEvent(entity));
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
