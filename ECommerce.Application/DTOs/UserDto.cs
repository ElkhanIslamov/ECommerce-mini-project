using System.ComponentModel.DataAnnotations;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public decimal Balanace { get; set; }
}

public class UserCreateDto:UserDto
{
    public UserType Type { get; set; }
    public string? Username { get; set; }
    public string Password { get; set; }
    public decimal Balanace { get; set; }

}

public class UserUpdateDto:UserDto
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public decimal Balanace { get; set; }

}