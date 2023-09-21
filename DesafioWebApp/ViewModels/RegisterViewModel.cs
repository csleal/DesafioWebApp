using System.ComponentModel.DataAnnotations;

namespace DesafioWebApp.ViewModels;

public class RegisterViewModel
{
    [Display(Name = "Endereço de Email")]
    [Required(ErrorMessage = "Endereço de email é obrigatório!")]
    public string Email { get; set; }
    [Display(Name = "Senha")]
    [Required]
    [DataType(DataType.Password)]
    public string Passwd { get; set; }
    [Display(Name = "Confirmação de Senha")]
    [Required(ErrorMessage = "A confirmação de senha é obrigatória!")]
    [DataType(DataType.Password)]
    [Compare("Passwd", ErrorMessage = "As senhas não coincidem")]
    public string ConfirmPasswd { get; set; }
}