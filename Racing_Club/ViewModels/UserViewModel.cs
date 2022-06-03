namespace Racing_Club.ViewModels;

public class UserViewModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public int? Pace { get; set; } // AKA Race Style
    public int? Mileage { get; set; } // Distance in KM/Miles
    public string ProfileImageUrl { get; set; }
}