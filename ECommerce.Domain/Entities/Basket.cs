namespace ECommerce.Domain.Entities
{
    public class Basket
    {
        public int UserId { get; set; }
        public List<BasketItem>? BasketItems { get; set; } = new List<BasketItem>();

    }
    public class BasketItem
    {
        public int ProductId { get; set; }
        public int ProductCount { get; set; }

    }
}
