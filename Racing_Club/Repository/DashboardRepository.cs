namespace Racing_Club.Repository;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<Race>> GetAllUserRaces() // Updated to use our extension from claim
    {
        var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId(); // Might come empty
        var userRaces = _context.Races.Where(x => x.AppUser.Id == currentUser);

        return userRaces.ToList();
    }

    public async Task<List<Club>> GetAllUserClubs()
    {
        var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId(); // Might come empty
        var userClubs = _context.Clubs.Where(x => x.AppUser.Id == currentUser);

        return userClubs.ToList();
    }
}