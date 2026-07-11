using ProductManagement.Domain.Events;

namespace ProductManagement.Application.Interfaces
{
    public interface IDomainEventHandler<in TEvent>
        where TEvent : IDomainEvent
    {
        Task HandleAsync(TEvent domainEvent);
    }
}
