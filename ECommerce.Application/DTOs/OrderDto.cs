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
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDto>? OrderItemDtos { get; set; } = new List<OrderItemDto>();
        public StatusType Status { get; set; }

    }
    public class OrderItemDto
    {
        public  int Id { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public decimal  TotalAmount { get; set; }
        public StatusType Status { get; set; } = StatusType.Pending;
    }
}
