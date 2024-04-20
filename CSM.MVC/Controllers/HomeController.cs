using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CSM.MVC.Models;
using CSM.MVC.Helpers;

namespace CSM.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, DataSeeder dataSeeder)
    {
        _logger = logger;
        //dataSeeder.SeedData(10, 5);
    }

    public IActionResult Index() 
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

