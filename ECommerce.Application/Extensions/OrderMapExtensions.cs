using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Extensions;

public static class OrderMapExtensions
{
    public static Order ToOrder(this OrderCreateDto createDto)
    {
        return new Order
        {
            UserId = createDto.UserId
        };
    }
    public static OrderDto ToOrderDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id
        };
    }

}
