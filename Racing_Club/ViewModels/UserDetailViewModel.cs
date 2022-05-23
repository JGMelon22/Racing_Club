namespace Racing_Club.ViewModels;

public class UserDetailViewModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public int? Pace { get; set; } // AKA Race Style
    public int? Mileage { get; set; } // Distance in KM/Miles
}