namespace Racing_Club.Repository;

public class RaceRepository : IRaceRepository
{
    // DI Implementation

    private readonly ApplicationDbContext _context;

    public RaceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Race>> GetAll()
    {
        return await _context.Races.ToListAsync();
    }

    public async Task<Race> GetByIdAsync(int id) // Fixed! Here we were not finding by the current Id at the page
    {
        return await _context.Races.Include(x => x.Address).FirstOrDefaultAsync(y => y.Id == id);
    }

    // No tracking
    public async Task<Race> GetByIdAsyncNoTracking(int id)
    {
        return await _context.Races.Include(x => x.Address)
            .AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Race>> GetAllRaceByCity(string city)
    {
        return await _context.Races.Where(x => x.Address.City.Contains(city)).ToListAsync();
    }

    public bool Add(Race race)
    {
        _context.AddAsync(race);
        return Save();
    }

    public bool Update(Race race)
    {
        _context.Update(race);
        return Save();
    }

    public bool Delete(Race race)
    {
        _context.Remove(race);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}