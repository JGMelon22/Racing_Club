using Racing_Club.Models;

namespace Racing_Club.Controllers
{
    public class ClubController : Controller
    {
        // Dep. Injection
        private readonly IClubRepository _clubRepository;

        public ClubController(IClubRepository clubRepository) // Refactoring to use the Repo. Pattern
        {
            _clubRepository = clubRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            return View(clubs);
        }

        // Details return code
        public async Task<IActionResult> Detail(int id)
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }
    }
}