using DesafioWebApp.Models;

namespace DesafioWebApp.ViewModels;

public class DashboardViewModel
{
    public List<Corrida> Corridas { get; set; }
    public List<Club> Clubs { get; set; }
}