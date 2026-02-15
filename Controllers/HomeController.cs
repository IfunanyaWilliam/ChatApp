using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Chat()
    {
        ViewData["HubUrl"] = _configuration["SignalR:HubUrl"];
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
}
