using DesafioWebApp.Data;
using DesafioWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioWebApp.Controllers;

public class CorridaController : Controller
{
    private readonly ApplicationDbContext _context;

    public CorridaController(ApplicationDbContext context)
    {
        _context = context;
    }
    // GET
    public IActionResult Index()
    {
        List<Corrida> corridas = _context.Corridas.ToList();
        return View(corridas);
    }
}