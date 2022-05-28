namespace Racing_Club.Controllers;

public class UserController : Controller
{
    private readonly IUsersRepository _usersRepository;

    public UserController(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    [HttpGet("users")]
    public async Task<IActionResult> Index()
    {
        // Bringing the DB calls and repository to the controller
        var users = await _usersRepository.GetAllUsers();
        List<UserViewModel> result = new List<UserViewModel>();
        foreach (var user in users)
        {
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Mileage = user.Mileage,
                ProfileImageUrl = user.ProfileImageUrl
            };
            result.Add(userViewModel);
        }

        return View(result);
    }
    
    // Details
    public async Task<IActionResult> Detail(string id)
    {
        var user = await _usersRepository.GetUserById(id);
        var userDetailViewModel = new UserDetailViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Pace = user.Pace,
            Mileage = user.Mileage
        };

        return View(userDetailViewModel);
    }
}