using Datasource.Ef.Models;
using Microsoft.EntityFrameworkCore;

namespace Datasource.Ef.Contexts;

public class DbContextWebDeploy : DbContext
{
    public DbContextWebDeploy(DbContextOptions<DbContextWebDeploy> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<User> Users { get; set; }
}