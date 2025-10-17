using APIDemo.Entities;
using Microsoft.EntityFrameworkCore;


namespace APIDemo.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
}
