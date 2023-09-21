using DesafioWebApp.Data;
using DesafioWebApp.Interfaces;
using DesafioWebApp.Models;

namespace DesafioWebApp.Repository;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<List<Corrida>> GetAllUserCorridas()
    {
        var curUser = _httpContextAccessor.HttpContext?.User;
        var userCorridas = _context.Corridas.Where(r => r.AppUser.Id == curUser.ToString());
        return userCorridas.ToList();
    }

    public async Task<List<Club>> GetAllUserClubs()
    {
        var curUser = _httpContextAccessor.HttpContext?.User;
        var userClubs = _context.Clubs.Where(r => r.AppUser.Id == curUser.ToString());
        return userClubs.ToList();
    }
}