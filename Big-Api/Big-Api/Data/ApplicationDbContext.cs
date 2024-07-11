using Big_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Big_Api.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

  public DbSet<AppEntity> appEntities { get; set; }
}
