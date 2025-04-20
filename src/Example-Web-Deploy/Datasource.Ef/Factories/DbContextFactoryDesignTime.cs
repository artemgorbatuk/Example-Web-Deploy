using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Datasource.Ef.Contexts;


/// <summary>
/// migration use
/// </summary>
public class DbContextFactoryDesignTime : IDesignTimeDbContextFactory<DbContextWebDeploy>
{
    public DbContextWebDeploy CreateDbContext(string[] args)
    {
        //var pathtoApi = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Client.WebMvc"));

        //var configuration = new ConfigurationBuilder()
        //    .SetBasePath(pathtoApi) // package: Microsoft.Extensions.Configuration.Json
        //    .AddJsonFile("appsettings.json")
        //    .Build();

        //var connectionString = configuration.GetConnectionString("DefaultConnection");


        var connectionString = "Host=web-deploy-postgres;Port=5432;Database=WebDeploy;Username=postgres;Password=Password1;";

        var optionsBuilder = new DbContextOptionsBuilder<DbContextWebDeploy>();
        optionsBuilder.UseNpgsql(connectionString);

        return new DbContextWebDeploy(optionsBuilder.Options);
    }
}