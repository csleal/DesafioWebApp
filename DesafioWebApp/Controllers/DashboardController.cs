using DesafioWebApp.Interfaces;
using DesafioWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DesafioWebApp.Controllers;

public class DashboardController : Controller
{
    private readonly IDashboardRepository _dashboardRespository;

    public DashboardController(IDashboardRepository dashboardRespository)
    {
        _dashboardRespository = dashboardRespository;
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

    public IActionResult EditUserProfile()
    {
        throw new NotImplementedException();
    }
}