using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Racing_Club.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser> // maps based at the model 
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Race> Races { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Address> Addresses { get; set; }
}