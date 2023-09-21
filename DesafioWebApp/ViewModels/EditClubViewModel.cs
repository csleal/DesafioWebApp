using DesafioWebApp.Data.Enum;
using DesafioWebApp.Models;

namespace DesafioWebApp.ViewModels;

public class EditClubViewModel
{
    public int Id{ get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public IFormFile Imagem { get; set; }
    public string? Url { get; set; }
    public int? EnderecoId { get; set; }
    public Endereco? Endereco { get; set; }
    public CategoriaClub CategoriaClub { get; set; }
}