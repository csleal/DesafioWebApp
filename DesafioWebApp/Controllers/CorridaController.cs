using DesafioWebApp.Data;
using DesafioWebApp.Interfaces;
using DesafioWebApp.Models;
using DesafioWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioWebApp.Controllers;

public class CorridaController : Controller
{
    private readonly ICorridaRepository _corridaRepository;
    private readonly IPhotoService _photoService;

    public CorridaController(ICorridaRepository corridaRepository, IPhotoService photoService)
    {
        _corridaRepository = corridaRepository;
        _photoService = photoService;
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
    public async Task<IActionResult> Create(CreateCorridaViewModel corridaVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _photoService.AddPhotoAsync(corridaVM.Imagem);

            var corrida = new Corrida
            {
                Titulo = corridaVM.Titulo,
                Descricao = corridaVM.Descricao,
                Imagem = result.Url.ToString(),
                Endereco = new Endereco
                {
                    Rua = corridaVM.Endereco.Rua,
                    Cidade = corridaVM.Endereco.Cidade,
                    Estado = corridaVM.Endereco.Estado
                }
            };
            _corridaRepository.Add(corrida);
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("","Upload de imagem falhou");
        }

        return View(corridaVM);
    }
}