using Microsoft.EntityFrameworkCore;
using Pix.Data;
using Pix.Models;

namespace Pix.Repositories;

public class AccountRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Account?> GetAccountByNumandBank(string number, int bankId)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.Number.Equals(number) && a.BankId.Equals(bankId));
    }

    public async Task<Account?> GetAccountByNum(string number)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.Number.Equals(number));
    }

    public async Task<Account?> GetAccountById(int id)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.Id.Equals(id));
    }

    public async Task<Account> CreateAccount(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return account;
    }
}