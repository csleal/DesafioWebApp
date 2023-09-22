using CloudinaryDotNet.Actions;
using DesafioWebApp.Interfaces;
using DesafioWebApp.Data;
using DesafioWebApp.Models;
using DesafioWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DesafioWebApp.Controllers;

public class DashboardController : Controller
{
    private readonly IDashboardRepository _dashboardRespository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPhotoService _photoService;

    public DashboardController(IDashboardRepository dashboardRespository, IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
    {
        _dashboardRespository = dashboardRespository;
        _httpContextAccessor = httpContextAccessor;
        _photoService = photoService;
    }
    
    private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVM, ImageUploadResult photoResult)
    {
        user.Id = editVM.Id;
        user.Ritmo = editVM.Ritmo;
        user.Quilometragem = editVM.Quilometragem;
        user.ProfileImageUrl = photoResult.Url.ToString();
        user.Cidade = editVM.Cidade;
        user.Estado = editVM.Estado;
    }
    
    public async Task<IActionResult> Index()
    {
        var userCorridas = await _dashboardRespository.GetAllUserCorridas();
        var userClubs = await _dashboardRespository.GetAllUserClubs();
        var dashboardViewModel = new DashboardViewModel()
        {
            Corridas = userCorridas,
            Clubs = userClubs
        };
        return View(dashboardViewModel);
    }

    public async Task<IActionResult> EditUserProfile()
    {
        var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
        var user = await _dashboardRespository.GetUserById(curUserId);
        if (user == null) return View("Error");
        var editUserViewModel = new EditUserDashboardViewModel()
        {
            Id = curUserId,
            Ritmo = user.Ritmo,
            Quilometragem = user.Quilometragem,
            ProfileImageUrl = user.ProfileImageUrl,
            Cidade = user.Cidade,
            Estado = user.Estado
        };
        return View(editUserViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Falha ao editar perfil");
            return View("EditUserProfile", editVM);
        }

        var user = await _dashboardRespository.GetByIdNoTracking(editVM.Id);

        if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
        {
            var photoResult = await _photoService.AddPhotoAsync(editVM.Imagem);
            
            MapUserEdit(user, editVM, photoResult);

            _dashboardRespository.Update(user);
            
            return RedirectToAction("Index");
        }
        else
        {
            try
            {
                await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não foi possível excluir a foto");
                return View(editVM);
            }
            var photoResult = await _photoService.AddPhotoAsync(editVM.Imagem);

            MapUserEdit(user, editVM, photoResult);

            _dashboardRespository.Update(user);

            return RedirectToAction("Index");
        }
        
    }
    
}