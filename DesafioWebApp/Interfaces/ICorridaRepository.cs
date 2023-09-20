using DesafioWebApp.Models;

namespace DesafioWebApp.Interfaces;

public interface ICorridaRepository
{
    Task<IEnumerable<Corrida>> GetAll();
    Task<Corrida> getByIdAsyncTask(int id);
    Task<IEnumerable<Corrida>> GetAllCorridasByCity(string city);
    bool Add(Corrida corrida);
    bool Update(Corrida corrida);
    bool Delete(Corrida corrida);
    bool Save();
}