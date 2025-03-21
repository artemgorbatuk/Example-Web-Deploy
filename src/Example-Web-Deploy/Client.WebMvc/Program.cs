using Datasource.Ef.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContextWebDeploy>(options =>
{
    if (!options.IsConfigured)
    {
        // во время запуска тестов без этой проверки
        // возникает ошибка 'Multiple database providers in service provider'.
        // InMemory создает свою конфигурацию.

        var connectionString = builder.Configuration.GetConnectionString("Default") ?? default!;

        options.EnableSensitiveDataLogging(false);
        options.UseLazyLoadingProxies(false);
        options.UseChangeTrackingProxies(false);
        options.UseSqlServer(connectionString);
    }
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();