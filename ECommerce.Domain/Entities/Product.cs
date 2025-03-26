using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities;

public class Product : Entity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public Category? Category { get; set; }

    public bool IsValid()
    {
        return Price > 0;
    }
}