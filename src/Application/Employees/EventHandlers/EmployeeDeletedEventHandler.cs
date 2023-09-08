using DevTest.Domain.Events;
using Microsoft.Extensions.Logging;

namespace DevTest.Application.Employees.EventHandlers;

public class EmployeeDeletedEventHandler : INotificationHandler<EmployeeDeletedEvent>
{
    private readonly ILogger<EmployeeDeletedEventHandler> _logger;

    public EmployeeDeletedEventHandler(ILogger<EmployeeDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(EmployeeDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DevTest Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
