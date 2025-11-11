using management.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace management.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}