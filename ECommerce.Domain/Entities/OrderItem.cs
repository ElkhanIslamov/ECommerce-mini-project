namespace ECommerce.Domain.Entities
{
    public class OrderItem: Entity
    {
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }

    }
}
