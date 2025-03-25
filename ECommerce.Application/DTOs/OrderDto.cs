using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDto>? OrderItemDtos { get; set; } = new List<OrderItemDto>();
        public StatusType Status { get; set; }

    }
    public class OrderCreateDto
    {
        public int UserId { get; set; }
         public StatusType Status { get; set; } = StatusType.Pending;
        public List<OrderItemCreateDto>? OrderItems { get; set; } = new();
    }

    public class OrderUpdateDto
    {
        public int Id { get; set; }
        public StatusType Status { get; set; }
    }
}
