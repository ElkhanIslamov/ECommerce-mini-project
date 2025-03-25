using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities
{
    public class User:Entity
    {
        public string? Username { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string ?Password { get; set; }
        [Required, DataType(DataType.Password)]
        public List<Order>? Orders { get; set; } = new List<Order>();
        public UserType UserType { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Balance { get; set; }
    }
}
