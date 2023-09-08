using DevTest.Application.Common.Interfaces;
using DevTest.Domain.Entities;
using DevTest.Domain.Events;

namespace DevTest.Application.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int DepartmentId { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = new Employee
        {
            DepartmentId = request.DepartmentId,
            Name = request.Name,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth
        };

        entity.AddDomainEvent(new EmployeeCreatedEvent(entity));

        _context.Employees.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
