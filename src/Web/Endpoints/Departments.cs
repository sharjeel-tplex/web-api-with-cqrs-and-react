using DevTest.Application.Departments.Commands.CreateDepartment;
using DevTest.Application.Departments.Commands.DeleteDepartment;
using DevTest.Application.Departments.Commands.UpdateDepartment;
using DevTest.Application.Departments.Queries.GetDepartments;


namespace DevTest.Web.Endpoints;

public class Departments : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetDepartments)
            .MapPost(CreateDepartment)
            .MapPut(UpdateDepartment, "{id}")
            .MapDelete(DeleteDepartment, "{id}");
    }

    public async Task<DepartmentsVm> GetDepartments(ISender sender)
    {
        return await sender.Send(new GetDepartmentsQuery());
    }

    public async Task<int> CreateDepartment(ISender sender, CreateDepartmentCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateDepartment(ISender sender, int id, UpdateDepartmentCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteDepartment(ISender sender, int id)
    {
        await sender.Send(new DeleteDepartmentCommand(id));
        return Results.NoContent();
    }
}
