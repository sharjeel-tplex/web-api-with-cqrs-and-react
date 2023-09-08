using DevTest.Domain.Entities;

namespace DevTest.Application.Employees.Queries.GetEmployeesWithPagination;

public class EmployeeDto
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public int DepartmentId { get; set; }
    public required string DepartmentName { get; set; }
    public DateTime DateOfBirth { get; set; }   
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
