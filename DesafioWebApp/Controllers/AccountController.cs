using DesafioWebApp.Data;
using DesafioWebApp.Models;
using DesafioWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DesafioWebApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ApplicationDbContext _context;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    [HttpGet]
    public IActionResult Login()
    {
        var reponse = new LoginViewModel();
        return View(reponse);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid) return View(loginViewModel);

        var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

        if (user != null)
        {
            //O usuário foi encontrado, verifique a senha
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Passwd);
            if (passwordCheck)
            {
                //Senha correta, login
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Passwd, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Corrida");
                }
            }
            //Senha Incorreta
            TempData["Error"] = "Credenciais erradas. Por favor, tente novamente";
            return View(loginViewModel);
        }
        //Usuário não encontrado
        TempData["Error"] = "Credenciais erradas. Por favor, tente novamente";
        return View(loginViewModel);
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        var reponse = new RegisterViewModel();
        return View(reponse);
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) return View(registerViewModel);

        var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
        if (user != null)
        {
            TempData["Error"] = "Este endereço de email já está em uso";
            return View(registerViewModel);
        }

        var newUser = new AppUser()
        {
            Email = registerViewModel.Email,
            UserName = registerViewModel.Email
        };
        var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Passwd);

        if (newUserResponse.Succeeded)
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);

        return RedirectToAction("Index", "Corrida");
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Corrida");
    }
    
}