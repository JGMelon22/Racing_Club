namespace Racing_Club.ViewModels;

public class EditRaceViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
    public string? URL { get; set; } // It is optional to avoid problems with update image controller
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public RaceCategory RaceCategory { get; set; }
}