using Microsoft.AspNetCore.Mvc;

namespace DesafioWebApp.Controllers;

public class ClubController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
