using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers;

public class HomeController : Controller
{

    public HomeController(ILogger<HomeController> logger)
    {
    }

    public string Index()
    {
        return "Hello World!";
    }
}
