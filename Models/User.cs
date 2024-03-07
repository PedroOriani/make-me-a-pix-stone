using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pix.Models;

public class User(string email, string password)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set;}
    
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;

    public User ToEntity() 
    {
        return new User(email: Email, password: Password);
    }
}