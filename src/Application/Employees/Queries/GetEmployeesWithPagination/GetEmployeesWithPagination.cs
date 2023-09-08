using DevTest.Application.Common.Interfaces;
using DevTest.Application.Common.Mappings;
using DevTest.Application.Common.Models;

namespace DevTest.Application.Employees.Queries.GetEmployeesWithPagination;

public record GetEmployeesWithPaginationQuery : IRequest<PaginatedList<EmployeeDto>>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int? DepartmentId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetEmployeesWithPaginationQueryHandler : IRequestHandler<GetEmployeesWithPaginationQuery, PaginatedList<EmployeeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<EmployeeDto>> Handle(GetEmployeesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Employees.AsQueryable();

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Name.Contains(request.Name));
        }

        if (!string.IsNullOrEmpty(request.Email))
        {
            query = query.Where(x => x.Email.Contains(request.Email));
        }

        if (request.DepartmentId.HasValue)
        {
            query = query.Where(x => x.DepartmentId == request.DepartmentId);
        }

        return await query
            .OrderBy(x => x.Name)
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
