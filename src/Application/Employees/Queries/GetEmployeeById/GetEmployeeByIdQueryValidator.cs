namespace DevTest.Application.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
{
    public GetEmployeeByIdQueryValidator()
    {

        RuleFor(x => x.EmployeeId)
            .GreaterThanOrEqualTo(1).WithMessage("Employee id at least greater than or equal to 1.");
    }
}
