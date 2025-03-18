namespace ECommerce.Application.DTOs;

public class OrderItemDto
{
    public int UserId { get; set; }
    public ProductDto Product { get; set; }
    public int Quantity { get; set; }
}

public class OrderItemCreateDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class OrderItemUpdateDto
{

}