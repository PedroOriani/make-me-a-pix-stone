using Pix.Data;
using Pix.Models;

namespace Pix.Repositories;

public class UserRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<User> CreateUsersync(User user)
    {
        _context.User.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }
}