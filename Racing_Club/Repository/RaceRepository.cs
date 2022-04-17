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

    public async Task<Race> GetByIdAsync(int id)
    {
        // Include will include a field trough a lazy loading method (Join)
        return await _context.Races.Include(x => x.Id == id).FirstOrDefaultAsync();
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