using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}

public class UserCreateDto
{
    public string Username { get; set; }
    [Required, DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required, DataType(DataType.Password)]
    public string Password { get; set; }
    [Required, DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}

public class UserUpdateDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    [Required, DataType(DataType.EmailAddress)]
    public string Password { get; set; }
    [Required, DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}