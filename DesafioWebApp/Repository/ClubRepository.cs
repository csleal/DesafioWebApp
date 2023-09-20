using DesafioWebApp.Data;
using DesafioWebApp.Interfaces;
using DesafioWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DesafioWebApp.Repository;

public class ClubRepository : IClubRepository
{
    private readonly ApplicationDbContext _context;

    public ClubRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Club>> GetAll()
    {
        return await _context.Clubs.ToListAsync();
    }

    public async Task<Club> getByIdAsyncTask(int id)
    {
        return await _context.Clubs.Include(i => i.Endereco).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Club>> GetClubByCity(string city)
    {
        return await _context.Clubs.Where(c => c.Endereco.Cidade.Contains(city)).ToListAsync();
    }

    public bool Add(Club club)
    {
        _context.Add(club);
        return Save();
    }

    public bool Update(Club club)
    {
        _context.Update(club);
        return Save();
    }

    public bool Delete(Club club)
    {
        _context.Remove(club);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}