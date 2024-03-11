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
        SELECT COUNT(""k"".""UserId"") AS ""QuantidadeDeChaves""
        FROM ""Keys"" ""k""
        JOIN ""Accounts"" ""a"" ON ""k"".""AccountId"" = ""a"".""Id""
        JOIN ""Banks"" ""b"" ON ""a"".""BankId"" = ""b"".""Id""
        WHERE ""k"".""UserId"" = {0}
        AND ""b"".""Id"" = {1}";

    int count = await _context.Keys.FromSqlRaw(query, userId, bankId).AsNoTracking().CountAsync();

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