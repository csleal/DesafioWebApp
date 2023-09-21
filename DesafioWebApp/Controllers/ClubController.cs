using DesafioWebApp.Data;
using DesafioWebApp.Interfaces;
using DesafioWebApp.Models;
using DesafioWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioWebApp.Controllers;

public class ClubController : Controller
{
    private readonly IClubRepository _clubRepository;
    private readonly IPhotoService _photoService;

    public ClubController(IClubRepository clubRepository, IPhotoService photoService)
    {
        _clubRepository = clubRepository;
        _photoService = photoService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        IEnumerable<Club> clubs = await _clubRepository.GetAll();
        return View(clubs);
    }

    public async Task<IActionResult> Detail(int id)
    {
        Club club = await _clubRepository.GetByIdAsync(id);
        return View(club);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClubViewModel clubVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _photoService.AddPhotoAsync(clubVM.Imagem);

            var club = new Club
            {
                Titulo = clubVM.Titulo,
                Descricao = clubVM.Descricao,
                Imagem = result.Url.ToString(),
                Endereco = new Endereco
                {
                    Rua = clubVM.Endereco.Rua,
                    Cidade = clubVM.Endereco.Cidade,
                    Estado = clubVM.Endereco.Estado
                }
            };
            _clubRepository.Add(club);
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Upload de imagem falhou");
        }

        return View(clubVM);

    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var club = await _clubRepository.GetByIdAsync(id);
        if (club == null) return View("Error");
        var clubVM = new EditClubViewModel
        {
            Titulo = club.Titulo,
            Descricao = club.Descricao,
            EnderecoId = club.EnderecoId,
            Endereco = club.Endereco,
            Url = club.Imagem,
            CategoriaClub = club.CategoriaClub
        };
        return View(clubVM);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Falha ao editar o clube");
            return View("Edit", clubVM);
        }

        var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);

        if (userClub != null)
        {
            try
            {
                await _photoService.DeletePhotoAsync(userClub.Imagem);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Could not delete photo");
                return View(clubVM);
            }

            var photoResult = await _photoService.AddPhotoAsync(clubVM.Imagem);

            var club = new Club
            {
                Id = id,
                Titulo = clubVM.Titulo,
                Descricao = clubVM.Descricao,
                Imagem = photoResult.Url.ToString(),
                EnderecoId = clubVM.EnderecoId,
                Endereco = clubVM.Endereco,
            };

            _clubRepository.Update(club);

            return RedirectToAction("Index");
        }
        else
        {
            return View(clubVM);
        }
    }
}