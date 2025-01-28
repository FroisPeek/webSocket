using Microsoft.EntityFrameworkCore;
using wsapi.Models;

namespace wsapi.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Users> Users { get; set; }
}