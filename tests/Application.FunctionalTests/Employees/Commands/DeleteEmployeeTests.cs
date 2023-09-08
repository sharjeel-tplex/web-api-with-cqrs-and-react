using DevTest.Application.Employees.Commands.CreateEmployee;
using DevTest.Application.Employees.Commands.DeleteEmployee;
using DevTest.Domain.Entities;

namespace DevTest.Application.FunctionalTests.Employees.Commands;

using static Testing;

public class DeleteEmployeeTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidEmployeeId()
    {
        var command = new DeleteEmployeeCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteEmployee()
    {
        var department = await GetDefautITDepartment();

        var employeeId = await SendAsync(new CreateEmployeeCommand
        {
            Name = "Alex",
            Email = "alex@abc.com",
            DepartmentId = department.Id
        });

        await SendAsync(new DeleteEmployeeCommand(employeeId));

        var list = await FindAsync<Employee>(employeeId);

        list.Should().BeNull();
    }
}
