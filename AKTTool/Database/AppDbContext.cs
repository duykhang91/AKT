using AKTTool.Models;
using System.Data.Entity;

namespace AKTTool.Database
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    public DbSet<Door> doors { get; set; }
  }
}
