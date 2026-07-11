using ProductManagement.Domain.Entities;

namespace ProductManagement.Domain.Events
{
    public class ProductCreatedEvent : IDomainEvent
    {
        public Product Product { get; }

        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }
    }
}
