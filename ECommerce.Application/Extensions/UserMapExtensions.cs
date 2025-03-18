using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Extensions;

public static class UserMapExtensions
{
    public static User ToUser(this UserCreateDto createDto)
    {
        return new User
        {
            Username = createDto.Username
        };
    }

    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            User = user.Username,
           
        };
    }
}
