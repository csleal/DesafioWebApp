using DesafioWebApp.Interfaces;
using DesafioWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DesafioWebApp.Controllers;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [HttpGet("users")]
    public async Task<IActionResult> Index()
    {
        var users = await _userRepository.GetAllUsers();
        List<UserViewModel> result = new List<UserViewModel>();
        foreach(var user in users)
        {
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Ritmo = user.Ritmo,
                Quilometragem = user.Quilometragem,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            result.Add(userViewModel);
        }
        return View(result);
    }

    public async Task<IActionResult> Detail(string id)
    {
        var user = await _userRepository.GetUserById(id);
        var userDetailViewModel = new UserDetailViewModel()
        {
            Id = user.Id,
            UserName = user.UserName,
            Pace = user.Ritmo,
            Quilometragem = user.Quilometragem,
        };
        return View(userDetailViewModel);
    }
}
