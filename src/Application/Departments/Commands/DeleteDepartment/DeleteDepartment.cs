using DevTest.Application.Common.Interfaces;

namespace DevTest.Application.Departments.Commands.DeleteDepartment;

public record DeleteDepartmentCommand(int Id) : IRequest;

public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Departments
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Departments.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
