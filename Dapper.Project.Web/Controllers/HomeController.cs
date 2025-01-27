using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DapperProject.Web.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace DapperProject.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    
    public IActionResult Error(string status , Exception ex)
    {
        var errorModel = new ErrorViewModel(status,ex.Message);
        errorModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        return View();
    }
    
}