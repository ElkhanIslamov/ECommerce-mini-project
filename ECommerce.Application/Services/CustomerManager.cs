using System.Linq.Expressions;
using ECommerce.Application.DTOs;
using ECommerce.Application.Extensions;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Application.Services;

public class CustomerManager : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerManager(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public void Add(CustomerCreateDto createDto)
    {
        var customer = createDto.ToCustomer();

        _repository.Add(customer);
    }

    public CustomerDto Get(Expression<Func<Customer, bool>> predicate)
    {
        var customer = _repository.Get(predicate);

        var customerDto = customer.ToCustomerDto();

        return customerDto;
    }

    public List<CustomerDto> GetAll(Expression<Func<Customer, bool>>? predicate, bool asNoTracking)
    {
        var customers = _repository.GetAll(predicate, asNoTracking);

        var customerDtoList = new List<CustomerDto>();

        foreach (var item in customers)
        {
            customerDtoList.Add(new CustomerDto
            {
                Id = item.Id,
                Username = item.Username
            });
        }

        return customerDtoList;
    }

    public CustomerDto GetById(int id)
    {
        var customer = _repository.GetById(id);

        var customerDto = new CustomerDto
        {
            Id = customer.Id,
            Username = customer.Username
        };

        return customerDto;
    }

    public void Remove(int id)
    {
        var existEntity = _repository.GetById(id);

        if (existEntity == null) throw new Exception("Not found");

        _repository.Remove(existEntity);
    }

    public void Update(CustomerUpdateDto updateDto)
    {
        var customer = new Customer
        {
            Id = updateDto.Id,
            Username = updateDto.Username
        };

        _repository.Update(customer);
    }
}
