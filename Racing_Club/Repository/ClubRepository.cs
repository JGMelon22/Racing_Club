namespace Racing_Club.Repository;

public class ClubRepository : IClubRepository
{
    // DI implementation
    private readonly ApplicationDbContext _context;

    public ClubRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Club>> GetAll()
    {
        return await _context.Clubs.ToListAsync();
    }

    public async Task<Club> GetByIdAsync(int id)
    {
        // When working with first or default, needs a lambda
        // Include will include a field trough a lazy loading method (Join)
        return await _context.Clubs.Include(x => x.Address).FirstOrDefaultAsync(y => y.Id == id);
    }

    // No Tracking
    public async Task<Club> GetByIdAsyncNoTracking(int id)
    {
        return await _context.Clubs.Include(x => x.Address).AsNoTracking()
            .FirstOrDefaultAsync(y => y.Id == id);
    }

    public async Task<IEnumerable<Club>> GetClubByCity(string city)
    {
        return await _context.Clubs.Where(x => x.Address.City.Contains(city)).ToListAsync();
    }

    public bool Add(Club club)
    {
        _context.AddAsync(club);
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
        return saved > 0 ? true : false; // 0 - false | 1 - true
    }
}