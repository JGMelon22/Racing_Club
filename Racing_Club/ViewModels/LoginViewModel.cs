namespace Racing_Club.ViewModels;

public class LoginViewModel
{
    // Basic validation trough validation annotations
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Email address is required")]
    public string EmailAddress { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}