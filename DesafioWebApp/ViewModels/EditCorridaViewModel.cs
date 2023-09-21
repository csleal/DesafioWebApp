using DesafioWebApp.Data.Enum;
using DesafioWebApp.Models;

namespace DesafioWebApp.ViewModels;

public class EditCorridaViewModel
{
    public int Id{ get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public IFormFile Imagem { get; set; }
    public string? Url { get; set; }
    public int? EnderecoId { get; set; }
    public Endereco? Endereco { get; set; }
    public CategoriaCorrida CategoriaCorrida { get; set; }
}