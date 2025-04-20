using Datasource.Ef.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDesignTimeDbContextFactory<DbContextWebDeploy>, DbContextFactoryDesignTime>();

builder.Services.AddDbContext<DbContextWebDeploy>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? default!;

    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorCodesToAdd: null);
    });

    options.EnableSensitiveDataLogging(true);
    options.EnableDetailedErrors(true);
    options.UseLazyLoadingProxies(false);
    options.UseChangeTrackingProxies(false);
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<DbContextWebDeploy>();
//    context.Database.Migrate();
//}

await app.RunAsync();