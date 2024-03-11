using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pix.Models;

public class Bank(string name, string token)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }

    public string Name { get; set; } = name;

    public string Token { get; set; } = token;

    public List<Account>? accounts;
}