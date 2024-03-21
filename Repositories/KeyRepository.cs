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
    
    public async Task<Key[]> CountUserKeys(int userId)
    {
        return await _context.Keys
        .Include(k => k.Account)
        .Where(k => k.UserId.Equals(userId)).ToArrayAsync();
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