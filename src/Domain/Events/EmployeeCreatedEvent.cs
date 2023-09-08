using DevTest.Domain.Entities;

namespace DevTest.Domain.Events;

public class EmployeeCreatedEvent : BaseEvent
{
    public EmployeeCreatedEvent(Employee employee)
    {
        Item = employee;
    }

    public Employee Item { get; }
}
