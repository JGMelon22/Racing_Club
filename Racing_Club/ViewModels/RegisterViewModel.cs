namespace Racing_Club.ViewModels;

public class RegisterViewModel
{
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Email Address is Required")]
    public string EmailAddress { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Confirm Password")]
    [Required(ErrorMessage = "You Must Confirm your Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
}