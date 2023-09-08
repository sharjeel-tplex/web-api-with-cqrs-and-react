using DevTest.Application.Employees.Queries.GetEmployeesWithPagination;
using DevTest.Domain.Entities;

namespace DevTest.Application.FunctionalTests.Employees.Queries;

using static Testing;

public class GetEmployeesWithPagination : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnTwoEmployees()
    {
        await RunAsDefaultUserAsync();

        var query = new GetEmployeesWithPaginationQuery() { PageNumber = 1, PageSize = 2  };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
    }

    [Test]
    public async Task ShouldReturnAllITDepartmentEmployees()
    {
        await RunAsDefaultUserAsync();
        var itDepartment = await GetDefautITDepartment();
        var hrDepartment = await GetDefautHRDepartment();


        await AddAsync(new Employee
        {
            Name = "Alex",
            Email = "alex@abc.com",
            DepartmentId = itDepartment.Id
        });
        await AddAsync(new Employee
        {
            Name = "Jhon",
            Email = "Jhon@abc.com",
            DepartmentId = itDepartment.Id
        });
        await AddAsync(new Employee
        {
            Name = "Jack",
            Email = "Jack@abc.com",
            DepartmentId = itDepartment.Id
        });

        await AddAsync(new Employee
        {
            Name = "Yaron",
            Email = "yaron@abc.com",
            DepartmentId = hrDepartment.Id,
        });

        var query = new GetEmployeesWithPaginationQuery() { DepartmentId = itDepartment.Id, PageSize = int.MaxValue, PageNumber = 1 };

        var result = await SendAsync(query);

        result.Items.Should().HaveCount(3);
    }

}
