using DevTest.Application.Common.Interfaces;
using DevTest.Domain.Events;

namespace DevTest.Application.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommand(int Id) : IRequest;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Employees.Remove(entity);

        entity.AddDomainEvent(new EmployeeDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }

}
