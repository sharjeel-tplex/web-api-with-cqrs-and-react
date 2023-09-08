using DevTest.Application.Employees.Commands.CreateEmployee;
using DevTest.Application.Employees.Queries.GetEmployeeById;
using DevTest.Domain.Entities;

namespace DevTest.Application.FunctionalTests.Employees.Queries;

using static Testing;

public class GetEmployeeByIdTest : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnEmployee()
    {

        var department = await GetDefautITDepartment();

        var command = new CreateEmployeeCommand
        {
            Name = "Alex",
            Email = "alex@abc.com",
            DepartmentId = department.Id
        };

        var id = await SendAsync(command);
        var query = new GetEmployeeByIdQuery () {  EmployeeId = id };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
    }

}
