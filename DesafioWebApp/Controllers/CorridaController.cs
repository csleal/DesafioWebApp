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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CorridaController(ICorridaRepository corridaRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
    {
        _corridaRepository = corridaRepository;
        _photoService = photoService;
        _httpContextAccessor = httpContextAccessor;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        IEnumerable<Corrida> corridas = await _corridaRepository.GetAll();
        return View(corridas);
    }
    public async Task<IActionResult> Detail(int id)
    {
        Corrida corrida = await _corridaRepository.GetByIdAsync(id);
        return View(corrida);
    }
    
    public IActionResult Create()
    {
        var curUserID = _httpContextAccessor.HttpContext?.User.GetUserId();
        var createCorridaViewModel = new CreateCorridaViewModel { AppUserId = curUserID };
        return View(createCorridaViewModel);
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
                AppUserId = corridaVM.AppUserId,
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
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var corrida = await _corridaRepository.GetByIdAsync(id);
        if (corrida == null) return View("Error");
        var corridaVM = new EditCorridaViewModel
        {
            Titulo = corrida.Titulo,
            Descricao = corrida.Descricao,
            EnderecoId = corrida.EnderecoId,
            Endereco = corrida.Endereco,
            Url = corrida.Imagem,
            CategoriaCorrida = corrida.CategoriaCorrida
        };
        return View(corridaVM);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditCorridaViewModel corridaVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Falha ao editar a corrida");
            return View("Edit", corridaVM);
        }

        var userCorrida = await _corridaRepository.GetByIdAsyncNoTracking(id);

        if (userCorrida != null)
        {
            try
            {
                await _photoService.DeletePhotoAsync(userCorrida.Imagem);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Could not delete photo");
                return View(corridaVM);
            }

            var photoResult = await _photoService.AddPhotoAsync(corridaVM.Imagem);

            var corrida = new Corrida
            {
                Id = id,
                Titulo = corridaVM.Titulo,
                Descricao = corridaVM.Descricao,
                Imagem = photoResult.Url.ToString(),
                EnderecoId = corridaVM.EnderecoId,
                Endereco = corridaVM.Endereco,
            };

            _corridaRepository.Update(corrida);

            return RedirectToAction("Index");
        }
        else
        {
            return View(corridaVM);
        }
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        var corridaDetails = await _corridaRepository.GetByIdAsync(id);
        if (corridaDetails == null) return View("Error");
        return View(corridaDetails);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteCorrida(int id)
    {
        var corridaDetails = await _corridaRepository.GetByIdAsync(id);
        if (corridaDetails == null) return View("Error");

        _corridaRepository.Delete(corridaDetails);
        return RedirectToAction("Index");
    }
    
}