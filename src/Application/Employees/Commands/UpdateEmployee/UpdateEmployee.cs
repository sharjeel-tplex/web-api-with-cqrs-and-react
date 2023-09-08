using DevTest.Application.Common.Interfaces;

namespace DevTest.Application.Employees.Commands.UpdateEmployee;

public record UpdateEmployeeCommand : IRequest
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public int DepartmentId { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees   
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.Email = request.Email;
        entity.DepartmentId = request.DepartmentId;
        entity.DateOfBirth = request.DateOfBirth;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
