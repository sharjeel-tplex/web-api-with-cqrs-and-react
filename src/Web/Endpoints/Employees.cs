using DevTest.Application.Common.Models;
using DevTest.Application.Employees.Queries.GetEmployeesWithPagination;
using DevTest.Application.Employees.Commands.CreateEmployee;
using DevTest.Application.Employees.Commands.DeleteEmployee;
using DevTest.Application.Employees.Commands.UpdateEmployee;
using DevTest.Application.Employees.Queries.GetEmployeeById;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DevTest.Web.Endpoints;

public class Employees : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetEmployeesWithPagination )
            .MapGet(GetEmployeeById, "{id}")
            .MapPost(CreateEmployee)
            .MapPut(UpdateEmployee, "{id}")
            .MapDelete(DeleteEmployee, "{id}");
    }

    public async Task<PaginatedList<EmployeeDto>> GetEmployeesWithPagination(ISender sender, [AsParameters]  GetEmployeesWithPaginationQuery query)
    {
        return await sender.Send(query);
    }
    public async Task<EmployeeDto> GetEmployeeById(ISender sender, int id)
    {
        return await sender.Send(new GetEmployeeByIdQuery { EmployeeId = id });
    }


    public async Task<int> CreateEmployee(ISender sender, CreateEmployeeCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateEmployee(ISender sender, int id, UpdateEmployeeCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.Ok("Success");
    }


    public async Task<IResult> DeleteEmployee(ISender sender, int id)
    {
        await sender.Send(new DeleteEmployeeCommand(id));
        return Results.Ok("Success");
    }
}
