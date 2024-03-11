using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pix.Models;

public class Key(string value, string type)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public long Id { get; set;}

    public string Value { get; set; } = value;

    public string Type { get; set; } = type;

    public int UserId { get; set; }

    public User? user;

    public int AccountId { get; set; }

    public Account? account;
}