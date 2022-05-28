namespace Racing_Club.Controllers;

public class DashboardController : Controller
{
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPhotosService _photosService;

    public DashboardController(IDashboardRepository dashboardRepository,
        IHttpContextAccessor httpContextAccessor, IPhotosService photosService)
    {
        _dashboardRepository = dashboardRepository;
        _httpContextAccessor = httpContextAccessor;
        _photosService = photosService;
    }

    public async Task<IActionResult> Index()
    {
        // Specific data to be return from the DB for each user
        var userRaces = await _dashboardRepository.GetAllUserRaces();
        var userClubs = await _dashboardRepository.GetAllUserClubs();

        // All in one package thanks to the view model :)
        var dashboardViewModel = new DashboardViewModel()
        {
            Races = userRaces,
            Clubs = userClubs
        };

        return View(dashboardViewModel);
    }

    // Edit User Profile
    public async Task<IActionResult> EditUserProfile()
    {
        var curUserId = _httpContextAccessor.HttpContext.User.GetUserId(); // Neat extension method btw
        /* AppUser */
        var user = await _dashboardRepository.GetUserById(curUserId);

        // Just in case if random error happen, will be redirect to an user friendly view
        if (user == null)
            return View("Error");

        var editUserViewModel = new EditUserDashboardViewModel
        {
            Id = curUserId,
            Pace = user.Pace,
            Mileage = user.Mileage,
            ProfileImage = user.ProfileImageUrl, // This 3 items will be hardcoded at first
            City = user.City,
            State = user.State
        };

        return View(editUserViewModel);
    }
}