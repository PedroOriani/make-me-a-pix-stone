using System.ComponentModel.DataAnnotations;
using Pix.Models;

namespace Pix.DTOs;

public class CreateKeyDTO
{
    [Required]
    public required string Value { get; set; }

    [Required]
    public required string Type { get; set; }

    [Required]
    public required string Cpf { get; set; }

    [Required]
    public required string Number { get; set; }

    [Required]
    public required string Agency { get; set; }

    public Key ToEntity()
    {
        return new Key(value: Value, type: Type);
    }
}