using DevTest.Application.Common.Interfaces;
using DevTest.Application.Common.Models;
using DevTest.Application.Common.Security;
using DevTest.Domain.Enums;

namespace DevTest.Application.Departments.Queries.GetDepartments;

[Authorize]
public record GetDepartmentsQuery : IRequest<DepartmentsVm>;

public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, DepartmentsVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDepartmentsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DepartmentsVm> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        return new DepartmentsVm
        {

            List = await _context.Departments
                .AsNoTracking()
                .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Name)
                .ToListAsync(cancellationToken)
        };
    }
}
