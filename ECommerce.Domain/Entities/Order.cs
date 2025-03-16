namespace ECommerce.Domain.Entities;

public class Order:Entity
{
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public Product? ProductName { get; set; }
    public int ProductCount { get; set; }
    public decimal? TotalPrice { get; set; }

}
