using Microsoft.AspNetCore.Mvc;

namespace DesafioWebApp.Controllers;

public class CorridaController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}