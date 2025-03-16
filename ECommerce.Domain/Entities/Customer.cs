using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Customer:Entity
    {
        public string Username { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
