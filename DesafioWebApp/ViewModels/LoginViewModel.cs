using System.ComponentModel.DataAnnotations;

namespace DesafioWebApp.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Endereço de Email")]
    [Required(ErrorMessage = "Endereço de email é obrigatório!")]
    public string Email { get; set; }
    [Display(Name = "Senha")]
    [Required]
    [DataType(DataType.Password)]
    public string Passwd { get; set; }
}