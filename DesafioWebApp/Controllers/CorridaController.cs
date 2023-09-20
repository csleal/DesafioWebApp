using DesafioWebApp.Data;
using DesafioWebApp.Interfaces;
using DesafioWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioWebApp.Controllers;

public class CorridaController : Controller
{
    private readonly ICorridaRepository _corridaRepository;

    public CorridaController(ICorridaRepository corridaRepository)
    {
        _corridaRepository = corridaRepository;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        IEnumerable<Corrida> corridas = await _corridaRepository.GetAll();
        return View(corridas);
    }
    public async Task<IActionResult> Detail(int id)
    {
        Corrida corrida = await _corridaRepository.getByIdAsyncTask(id);
        return View(corrida);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Corrida corrida)
    {
        if (!ModelState.IsValid)
        {
            return View(corrida);
        }

        _corridaRepository.Add(corrida);
        return RedirectToAction("Index");
    }
    
}