using Microsoft.AspNetCore.Mvc;

namespace Racing_Club.Controllers
{
    public class RaceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}