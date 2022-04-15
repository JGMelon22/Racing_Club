using Racing_Club.Models;

namespace Racing_Club.Controllers
{
    public class ClubController : Controller
    {
        // Dep. Injection
        private readonly ApplicationDbContext _context;

        public ClubController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Club> clubs = _context.Clubs.ToList();
            return View(clubs);
        }

        // Details return code
        public IActionResult Detail(int id)
        {
            // Include will include a field trough a lazy loading method (Join)
            Club club = _context.Clubs.Include(x => x.Address).FirstOrDefault(y => y.Id == id);
            return View(club);
        }
    }
}