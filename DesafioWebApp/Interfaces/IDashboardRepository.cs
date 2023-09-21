using DesafioWebApp.Models;

namespace DesafioWebApp.Interfaces;

public interface IDashboardRepository
{
    Task<List<Corrida>> GetAllUserCorridas();

    Task<List<Club>> GetAllUserClubs();
}