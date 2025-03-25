using System.Linq.Expressions;
using ECommerce.Application.DTOs;
using ECommerce.Application.Extensions;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Application.Services;

public class UserManager : IUserService
{
    private readonly IUserRepository _repository;

    public UserManager(IUserRepository repository)
    {
        _repository = repository;
    }

    public void Add(UserCreateDto createDto)
    {
        var user = createDto.ToUser();

        _repository.Add(user);
    }

    public UserDto Get(Expression<Func<User?, bool>> predicate)
    {
        var user = _repository.Get(predicate);

        var userDto = user.ToUserDto();

        return userDto;
    }

    public List<UserDto> GetAll(Expression<Func<User, bool>>? predicate = null, bool asNoTracking = false)
    {
        var users = _repository.GetAll(predicate, asNoTracking);

        var userDtoList = new List<UserDto>();

        foreach (var item in users)
        {
            userDtoList.Add(new UserDto
            {
                Id = item.Id,
                Username = item.Username
            });
        }

        return userDtoList;
    }

    public UserDto GetById(int id)
    {
        var user = _repository.GetById(id);

        var userDto = new UserDto
        {
            Id = user.Id,
            Username = user.Username
        };

        return userDto;
    }

    public void Remove(int id)
    {
        var existEntity = _repository.GetById(id);

        if (existEntity == null) throw new Exception("Not found");

        _repository.Remove(existEntity);
    }

    public void Update(UserUpdateDto updateDto)
    {
        var user = new User
        {
            Id = updateDto.Id,
            Username = updateDto.Username
        };

        _repository.Update(user);
    }
}
