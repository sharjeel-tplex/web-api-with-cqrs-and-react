using DevTest.Application.Common.Models;

namespace DevTest.Application.Departments.Queries.GetDepartments;

public class DepartmentsVm
{
    public IReadOnlyCollection<DepartmentDto> List { get; init; } = Array.Empty<DepartmentDto>();
}
