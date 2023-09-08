using DevTest.Application.Common.Exceptions;
using DevTest.Application.Departments.Queries.GetDepartmentById;
using DevTest.Application.Departments.Queries.GetDepartments;
using DevTest.Application.Employees.Commands.CreateEmployee;
using DevTest.Domain.Entities;

namespace DevTest.Application.FunctionalTests.Employees.Commands;

using static Testing;

public class CreateEmployeeTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateEmployeeCommand() { Email = null, Name = null };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateEmployee()
    {
        var userId = await RunAsDefaultUserAsync();

        var department = await GetDefautITDepartment();

        var command = new CreateEmployeeCommand
        {
            Name = "Alex",
            Email = "alex@abc.com",
            DepartmentId = department.Id
        };

        var id = await SendAsync(command);

        var list = await FindAsync<Employee>(id);

        list.Should().NotBeNull();
        list!.Name.Should().Be(command.Name);
        list.CreatedBy.Should().Be(userId);
        list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
