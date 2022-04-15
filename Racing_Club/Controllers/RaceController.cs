using Racing_Club.Models;

namespace Racing_Club.Controllers
{
    public class RaceController : Controller
    {
        // Dep. Injection
        private readonly ApplicationDbContext _context;

        public RaceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Race> races = _context.Races.ToList();
            return View(races);
        }

        // Details return code
        public IActionResult Detail(int id)
        {
            // Include will include a field trough a lazy loading method (Join)
            Race race = _context.Races.Include(x => x.Address).FirstOrDefault(y => y.Id == id);
            return View(race);
        }
    }
}