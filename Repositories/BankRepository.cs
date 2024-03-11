using Microsoft.EntityFrameworkCore;
using Pix.Data;
using Pix.Models;

namespace Pix.Repositories;

public class BankRepository (AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Bank?> GetBankByToken (string token)
    {
        return await _context.Banks.FirstOrDefaultAsync(b => b.Token.Equals(token));
    }

    public async Task<Bank?> GetBankById (int id)
    {
        return await _context.Banks.FirstOrDefaultAsync(b => b.Id.Equals(id));
    }
}