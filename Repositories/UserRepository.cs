using Pix.Models;

namespace Pix.Repositories;

public class UserRepository
{
    private readonly User[] users = [];

    public User CreateUser(User user)
    {
        _ = users.Append(user);
        return user;
    }
}