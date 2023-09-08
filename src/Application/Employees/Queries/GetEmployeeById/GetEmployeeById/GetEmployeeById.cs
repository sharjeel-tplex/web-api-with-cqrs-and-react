using DevTest.Application.Common.Interfaces;
using DevTest.Application.Common.Mappings;
using DevTest.Application.Common.Models;
using DevTest.Application.Employees.Queries.GetEmployeesWithPagination;

namespace DevTest.Application.Employees.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery : IRequest<EmployeeDto>
{
    public int EmployeeId { get; init; }
}

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {

        var query =  from employee in _context.Employees.AsQueryable()
        where employee.Id == request.EmployeeId
               select employee;

        return await Task.FromResult(_mapper.Map<EmployeeDto>(query.FirstOrDefault()));
    }
}
