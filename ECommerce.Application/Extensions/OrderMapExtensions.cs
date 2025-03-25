using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Extensions
{
    public static class OrderMapExtensions
    {
        public static Order ToOrder(this OrderCreateDto createDto)
        {
            return new Order
            {
                UserId = createDto.UserId,
                CreatedAt = DateTime.UtcNow,
                OrderItems = createDto.OrderItems
                    .Select(x => new OrderItem { ProductId = x.ProductId, Quntity = x.Quantity })
                    .ToList()
            };
        }

        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id
            };
        }

        public static OrderCreateDto ToOrderCreateDto(this Basket basket)
        {
            return new OrderCreateDto
            {
                UserId = basket.UserId,
                OrderItems = basket.BasketItems
                    .Select(x => new OrderItemCreateDto
                    {
                        ProductId = x.ProductId,
                        Quantity = x.ProductCount
                    })
                    .ToList()
            };
        }

        public static List<OrderItemCreateDto> ToOrderItemCreateDtoList(this Basket basket)
        {
            return basket.BasketItems.Select(x => new OrderItemCreateDto
            {
                ProductId = x.ProductId,
                Quantity = x.ProductCount
            }).ToList();
        }
    }
}

