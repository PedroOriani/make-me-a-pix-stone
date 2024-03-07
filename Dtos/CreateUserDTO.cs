using System.ComponentModel.DataAnnotations;
using Pix.Models;

namespace Pix.DTOs;

public class CreateUserDTO
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [RegularExpression(@"^.{3,}$")]
    public required string Password { get; set; }

    public User ToEntity()
    {
        return new User(email: Email, password: Password);
    }
}