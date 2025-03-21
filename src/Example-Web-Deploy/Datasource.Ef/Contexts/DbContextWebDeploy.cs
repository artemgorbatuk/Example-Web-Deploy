using Datasource.Ef.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Datasource.Ef.Contexts;

public class DbContextWebDeploy : DbContext
{
    public DbContextWebDeploy(DbContextOptions<DbContextWebDeploy> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<User> Users { get; set; }
}