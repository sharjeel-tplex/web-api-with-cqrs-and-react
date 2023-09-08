using DevTest.Domain.Entities;

namespace DevTest.Application.Departments.Queries.GetDepartments;

public class DepartmentDto

{
    public int Id { get; init; }

    public required string Name { get; set; }

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset LastModified { get; set; }


    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Department, DepartmentDto>();
        }
    }
}
