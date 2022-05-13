namespace Racing_Club.Controllers;

public class DashboardController : Controller
{
    private readonly IDashboardRepository _dashboardRepository;

    public DashboardController(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
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
}