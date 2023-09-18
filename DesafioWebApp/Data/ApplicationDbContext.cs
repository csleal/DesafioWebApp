using DesafioWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioWebApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Corrida> Corridas { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
}