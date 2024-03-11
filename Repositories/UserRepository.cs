using Microsoft.EntityFrameworkCore;
using Pix.Data;
using Pix.Models;

namespace Pix.Repositories;

public class UserRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<User?> GetUserByCpf(string cpf)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Cpf.Equals(cpf));
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
    }
}