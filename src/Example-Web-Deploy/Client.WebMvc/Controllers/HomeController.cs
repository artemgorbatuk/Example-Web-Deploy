using Client.WebMvc.Models;
using Datasource.Ef.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Client.WebMvc.Controllers;

public class HomeController : Controller
{
    private readonly DbContextWebDeploy context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(DbContextWebDeploy context, ILogger<HomeController> logger)
    {
        this.context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> UserList()
    {
        var result = await context.Users.ToListAsync();

        var response = result.Select(p => new UserModelView
        {
            Id = p.Id,
            Name = p.Name
        }).ToList();

        return View(response);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
