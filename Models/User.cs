namespace Pix.Models;

public class User
{
    public User (string email, string password)
    {
        Email = email;
        Password = password;
    }
    public string Email { get; set; }
    public string Password { get; set; }

    public User ToEntity() 
    {
        return new User(email: Email, password: Password);
    }
}