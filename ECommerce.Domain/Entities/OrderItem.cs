namespace ECommerce.Domain.Entities
{
    public class OrderItem: Entity
    {

        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public Product? ProductId { get; set; }
        public int ProductCount { get; set; }
        public int Quntity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}
