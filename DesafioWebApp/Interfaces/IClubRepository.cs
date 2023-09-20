using DesafioWebApp.Models;

namespace DesafioWebApp.Interfaces;

public interface IClubRepository
{
    Task<IEnumerable<Club>> GetAll();
    Task<Club> getByIdAsyncTask(int id);
    Task<IEnumerable<Club>> GetClubByCity(string city);
    bool Add(Club club);
    bool Update(Club club);
    bool Delete(Club club);
    bool Save();
}