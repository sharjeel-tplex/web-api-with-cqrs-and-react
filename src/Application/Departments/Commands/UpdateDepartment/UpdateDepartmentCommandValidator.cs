using DevTest.Application.Common.Interfaces;

namespace DevTest.Application.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateDepartmentCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueName)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueName(UpdateDepartmentCommand model, string name, CancellationToken cancellationToken)
    {
        return await _context.Departments
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Name != name, cancellationToken);
    }
}
