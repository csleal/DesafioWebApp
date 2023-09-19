using System.ComponentModel.DataAnnotations;

namespace DesafioWebApp.Models;

public class AppUser
{
    [Key]
    public string Id { get; set; }
    public int? Pace { get; set; }
    public int? Quilometragem { get; set; }
    public Endereco? Endereco { get; set; }
    public ICollection<Club> Clubs { get; set; }
    public ICollection<Corrida> Corridas { get; set; }
}