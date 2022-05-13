using Racing_Club.Data.Enum;

namespace Racing_Club.Models;

public class Club
{
    [Key] public int Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }

    [ForeignKey("Address")] public int AddressId { get; set; }

    public Address Address { get; set; }
    public ClubCategory ClubCategory { get; set; }

    [ForeignKey("AppUser")] public string? AppuserId { get; set; }

    public AppUser? AppUser { get; set; }
}