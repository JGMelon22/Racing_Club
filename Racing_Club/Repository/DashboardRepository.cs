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

    // Getting a user by Id
    public async Task<AppUser> GetUserById(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser> GetByIdNoTracking(string id)
    {
        return await _context.Users
            .Where(x => x.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public bool Update(AppUser user)
    {
        _context.Users.Update(user);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false; // 0 - false | 1 - true
    }
}