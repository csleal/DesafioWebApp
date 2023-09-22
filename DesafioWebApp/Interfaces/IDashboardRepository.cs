using DesafioWebApp.Models;

namespace DesafioWebApp.Interfaces;

public interface IDashboardRepository
{
    Task<List<Corrida>> GetAllUserCorridas();

    Task<List<Club>> GetAllUserClubs();

    Task<AppUser> GetUserById(string id);
    Task<AppUser> GetByIdNoTracking(string id);
    bool Update(AppUser user);
    bool Save();
}