using Microsoft.AspNetCore.Mvc;

namespace Racing_Club.Controllers
{
    public class ClubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}