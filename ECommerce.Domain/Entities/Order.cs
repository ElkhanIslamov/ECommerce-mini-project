using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities;

public class Order:Entity
{
    public int UserId { get; set; }
    public User? User { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
    public StatusType StatusType { get; set; }

}
