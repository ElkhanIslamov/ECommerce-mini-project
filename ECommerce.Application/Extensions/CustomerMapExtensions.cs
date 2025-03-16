using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Extensions;

public static class CustomerMapExtensions
{
    public static Customer ToCustomer(this CustomerCreateDto createDto)
    {
        return new Customer
        {
            Username = createDto.Username
        };
    }

    public static CustomerDto ToCustomerDto(this Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            Username = customer.Username,
            Email = customer.Email    
        };
    }
}
