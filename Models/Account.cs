using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pix.Models;

public class Account(string agency, string number)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set;}

    public string Agency { get; set; } = agency;

    public string Number { get; set; } = number;

    public int UserId { get; set; }

    public User? user;

    public int BankId { get; set; }

    public Bank? bank;
    
    public List<Key>? keys;
}