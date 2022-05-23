namespace Racing_Club.Repository;

public class UserRepository : IUsersRepository
{
    // DI
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppUser>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<AppUser> GetUserById(string id)
    {
        return await _context.Users.FindAsync(id);
        // .Include(x => x.Id).
        // .FirstOrDefaultAsync(y => y.Id == id);
    }

    public bool Add(AppUser user)
    {
        throw new NotImplementedException();
    }

    public bool Update(AppUser user)
    {
        _context.Update(user);
        return Save();
    }

    public bool Delete(AppUser user)
    {
        throw new NotImplementedException();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false; // Ternary operator
    }
}