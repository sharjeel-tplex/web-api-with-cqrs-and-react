using DevTest.Application.Common.Interfaces;
using DevTest.Domain.Entities;

namespace DevTest.Application.Departments.Commands.CreateDepartment;

public record CreateDepartmentCommand : IRequest<int>
{
    public required string Name { get; init; }
}

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Department();

        entity.Name = request.Name;

        _context.Departments.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
