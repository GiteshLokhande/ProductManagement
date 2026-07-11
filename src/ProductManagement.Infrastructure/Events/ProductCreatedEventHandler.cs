using Microsoft.Extensions.Logging;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Events;

namespace ProductManagement.Infrastructure.Events
{
    public class ProductCreatedEventHandler : IDomainEventHandler<ProductCreatedEvent>
    {
        private readonly ILogger<ProductCreatedEventHandler> _logger;

        public ProductCreatedEventHandler(
            ILogger<ProductCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(ProductCreatedEvent domainEvent)
        {
            _logger.LogInformation(
                "Domain Event: Product '{ProductName}' created at {Time}",
                domainEvent.Product.ProductName,
                domainEvent.OccurredOn);

            return Task.CompletedTask;
        }
    }
}
