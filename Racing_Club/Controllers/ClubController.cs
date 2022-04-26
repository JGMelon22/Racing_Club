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

        // CREATE
        public IActionResult Create()
        {
            return View();
        }

        // POST Request
        [HttpPost]
        public async Task<IActionResult> Create(Club club)
        {
            if (!ModelState.IsValid)  // Basic validation...
                return View(club);

            _clubRepository.Add(club); // If do not pass, will return to the "Index" view
            return RedirectToAction("Index");
        }
    }
}