using Microsoft.EntityFrameworkCore;
using Pix.Models;

namespace Pix.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
{
    public DbSet<User> User { get; set;}
}