// Handles the login logic

namespace Racing_Club.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ApplicationDbContext _context;

    // DI
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    /// <summary>
    /// In theory, AddSession and AddMemoryCache already does it
    /// But we want to make sure the user and his password will be kept
    /// while he/she is using after some time using
    /// </summary>
    /// <returns></returns
    [HttpGet]
    public IActionResult Login()
    {
        var response = new LoginViewModel();
        return View(response);
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
            return View(loginViewModel);

        var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

        if (user != null)
        {
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

            // If check - User founded, check pwd
            if (passwordCheck)
            {
                // Pwd ok, sign in
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Race"); // Action - Controller
                }
            }

            // If it fails - Pwd is invalid
            TempData["Error"] = "Wrong password. Try again.";
            return View(loginViewModel);
        }

        // User was not found
        TempData["Error"] = "Informed credentials are wrong. Please, try again.";
        return View(loginViewModel);
    }
}