using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerce.Application.Interfaces;

public interface ICustomerService
{
    CustomerDto GetById(int id);
    CustomerDto Get(Expression<Func<Customer, bool>> predicate);
    List<CustomerDto> GetAll(Expression<Func<Customer, bool>>? predicate, bool asNoTracking);
    void Add(CustomerCreateDto createDto);
    void Update(CustomerUpdateDto updateDto);
    void Remove(int id);
}
