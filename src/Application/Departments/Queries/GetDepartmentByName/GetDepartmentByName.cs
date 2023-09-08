using DevTest.Application.Common.Interfaces;
using DevTest.Application.Common.Models;
using DevTest.Application.Common.Security;
using DevTest.Application.Departments.Queries.GetDepartments;
using DevTest.Application.Employees.Queries.GetEmployeesWithPagination;
using DevTest.Domain.Enums;

namespace DevTest.Application.Departments.Queries.GetDepartmentById;

[Authorize]
public record GetDepartmentByNameQuery : IRequest<DepartmentDto>
{
    public string Name { get; set; }
}

public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentByNameQuery, DepartmentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDepartmentsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DepartmentDto> Handle(GetDepartmentByNameQuery request, CancellationToken cancellationToken)
    {
        var query = from department in _context.Departments.AsQueryable()
                    where department.Name.Contains(request.Name)
                    select department;

        return await Task.FromResult(_mapper.Map<DepartmentDto>(query.FirstOrDefault()));
    }
}
