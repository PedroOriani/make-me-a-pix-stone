using Microsoft.EntityFrameworkCore;
using Pix.Data;
using Pix.Models;

namespace Pix.Repositories;

public class KeyRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Key> Createkey(Key key)
    {
        _context.Keys.Add(key);
        await _context.SaveChangesAsync();
        return key;
    }

    public async Task<int> CountBankUserKeys(int userId, int bankId)
{
    string query = @"
    SELECT * FROM ""Keys"" 
    WHERE ""UserId"" = " + userId + @" 
    AND ""AccountId"" IN 
        ( SELECT ""Id"" 
        FROM ""Accounts"" 
        WHERE ""UserId"" = " + userId + @"
        AND ""BankId"" = " + bankId + @")";

    int count = await _context.Keys.FromSqlRaw(query).AsNoTracking().CountAsync();

    return count;
}
    
    public async Task<int> CountUserKeys(int userId)
    {
        int count = await _context.Keys
        .Where(k => k.UserId.Equals(userId)).CountAsync();
        return count;
    }

    public async Task<Key?> GetKeyByValue(string value)
    {
        return await _context.Keys.FirstOrDefaultAsync(k => k.Value.Equals(value));
    }

    public async Task<Key?> GetKeyByTypeAndValue(string type, string value)
    {
        return await _context.Keys.FirstOrDefaultAsync(k => k.Type == type && k.Value == value);
    }
}