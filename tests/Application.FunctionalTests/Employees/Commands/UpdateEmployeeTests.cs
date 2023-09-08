using DevTest.Application.Common.Exceptions;
using DevTest.Application.Employees.Commands.CreateEmployee;
using DevTest.Application.Employees.Commands.UpdateEmployee;
using DevTest.Domain.Entities;

namespace DevTest.Application.FunctionalTests.Employees.Commands;

using static Testing;

public class UpdateEmployeeTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidEmployeeId()
    {
        var command = new UpdateEmployeeCommand { Id = 99, Name = "Alex", Email = "alex@abc.com", DepartmentId = 1 };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }


    [Test]
    public async Task ShouldUpdateEmployee()
    {
        var department = await GetDefautITDepartment();

        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateEmployeeCommand
        {
            Name = "Jhon",
            Email = "jhon@abc.com",
            DepartmentId = department.Id
        });

        var command = new UpdateEmployeeCommand
        {
            Id = listId,
            Name = "Updated Jhon Name",
            Email = "jhon2@abc.com",
            DepartmentId = department.Id
        };

        await SendAsync(command);

        var list = await FindAsync<Employee>(listId);

        list.Should().NotBeNull();
        list!.Name.Should().Be(command.Name);
        list.LastModifiedBy.Should().NotBeNull();
        list.LastModifiedBy.Should().Be(userId);
        list.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
