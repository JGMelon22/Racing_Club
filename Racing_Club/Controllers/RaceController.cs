namespace Racing_Club.Controllers
{
    public class RaceController : Controller
    {
        // Dep. Injection
        private readonly IRaceRepository _raceRepository;

        public RaceController(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceRepository.GetAll();
            return View(races); // Aqui
        }

        // Details return code
        public async Task<IActionResult> Detail(int id)
        {
            // Include will include a field trough a lazy loading method (Join)
            Race race = await _raceRepository.GetByIdAsync(id);
            return View(race);
        }

        // CREATE (View call)
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
                return View(race);

            _raceRepository.Add(race);
            return RedirectToAction("Index");
        }
    }
}