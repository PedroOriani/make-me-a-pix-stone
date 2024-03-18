using Microsoft.EntityFrameworkCore;
using Pix.Models;

namespace Pix.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Key> Keys { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Payment> Payments { get; set; }
}