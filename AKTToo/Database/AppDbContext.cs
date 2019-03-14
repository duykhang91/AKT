using AKTTool.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AKTTool.Database
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    public DbSet<Door> doors { get; set; }
  }

  public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
  {
    public AppDbContext CreateDbContext(string[] args)
    {
      var config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
      var builder = new DbContextOptionsBuilder<AppDbContext>();
      var configuration = config.Build();

      builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
      return new AppDbContext(builder.Options);
    }
  }
}
