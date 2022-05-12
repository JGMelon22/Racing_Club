namespace Racing_Club.Models;

public class AppUser : IdentityUser
{
    public int? Pace { get; set; } // Should've changed to "Race Style" 
    public int? Mileage { get; set; } // Distance - Miles/Kms
    [ForeignKey("Address")] 
    public int? AddressId { get; set; } // We will link from address table
    public Address? Address { get; set; }
    public ICollection<Club> Clubs { get; set; }
    public ICollection<Race> Races { get; set; }
}