namespace DevTest.Domain.Events;

public class EmployeeDeletedEvent : BaseEvent
{
    public EmployeeDeletedEvent(Employee employee)
    {
        Employee = employee;
    }

    public Employee Employee{ get; }
}
