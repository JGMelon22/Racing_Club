using Microsoft.AspNetCore.Mvc;
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
    }
}