using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Extensions;

public static class OrderMapExtensions
{
    public static Order ToOrder(this OrderCreateDto createDto)
    {
        return new Order
        {
            UserId = createDto.UserId,
            CreatedAt = DateTime.UtcNow,
            OrderItems = createDto.OrderItems.Select(x=>new OrderItem {ProductId=x.ProductId, Quntity=x.Quantity }).ToList()
        };
    }
    public static OrderDto ToOrderDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id
        };
    }
    //public static Basket ToOrderCreateDto(this OrderCreateDto createDto)
    //{
    //    return new Basket
    //    {
    //        UserId = createDto.UserId,
    //        BasketItems = createDto.  //Select(x => new BasketItem { ProductId = x.ProductId, ProductCount = x.ProductCount }).ToList()

    //    };
    //}

    public static OrderItemCreateDto ToOrderItemCreateDto(this Basket basket)
    {
        return new OrderItemCreateDto
        {
            OrderId = basket.UserId,
            ProductId = basket.BasketItems.Select(x => new BasketItem
            {
               ProductCount = x.ProductCount,
               ProductId = x.ProductId,

            })
        };

            //UserId = basket.UserId,
            //OrderItems = basket.BasketItems.Select(x => new OrderItem
            //{
            //    Id = x.ProductId,
            //    ProductCount = x.ProductCount
            //})
    }

}
