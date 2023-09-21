using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DesafioWebApp.Models;

public class AppUser : IdentityUser
{
    public int? Pace { get; set; }
    public int? Quilometragem { get; set; }
    [ForeignKey("Endereco")]
    public int EnderecoId { get; set; }
    public Endereco? Endereco { get; set; }
    public ICollection<Club> Clubs { get; set; }
    public ICollection<Corrida> Corridas { get; set; }
}