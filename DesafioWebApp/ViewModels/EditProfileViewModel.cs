namespace DesafioWebApp.ViewModels;

public class EditProfileViewModel
{
    public int? Pace { get; set; }
    public int? Quilometragem { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public IFormFile? Imagem { get; set; }
}