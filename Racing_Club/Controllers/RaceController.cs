using Microsoft.AspNetCore.Mvc;
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
    }
}