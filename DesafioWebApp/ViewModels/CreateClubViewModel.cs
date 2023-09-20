using DesafioWebApp.Data.Enum;
using DesafioWebApp.Models;

namespace DesafioWebApp.ViewModels;

public class CreateClubViewModel
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Endereco Endereco { get; set; }
    public IFormFile Imagem { get; set; }
    public CategoriaClub CategoriaClub { get; set; }
}