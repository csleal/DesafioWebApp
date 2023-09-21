using DesafioWebApp.Data;
using DesafioWebApp.Interfaces;
using DesafioWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioWebApp.Repository;

public class CorridaRepository : ICorridaRepository
{
    private readonly ApplicationDbContext _context;

    public CorridaRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Corrida>> GetAll()
    {
        return await _context.Corridas.ToListAsync();
    }

    public async Task<Corrida> GetByIdAsync(int id)
    {
        return await _context.Corridas.Include(i => i.Endereco).FirstOrDefaultAsync(i => i.Id == id);
    }
    
    public async Task<Corrida> GetByIdAsyncNoTracking(int id)
    {
        return await _context.Corridas.Include(i => i.Endereco).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
    }

    public Task<IEnumerable<Corrida>> GetAllCorridasByCity(string city)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Corrida>> GetCorridaByCity(string city)
    {
        return await _context.Corridas.Where(c => c.Endereco.Cidade.Contains(city)).ToListAsync();
    }

    public bool Add(Corrida corrida)
    {
        _context.Add(corrida);
        return Save();
    }

    public bool Update(Corrida corrida)
    {
        _context.Update(corrida);
        return Save();
    }

    public bool Delete(Corrida corrida)
    {
        _context.Remove(corrida);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}