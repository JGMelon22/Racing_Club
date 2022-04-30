using Racing_Club.ViewModels;

namespace Racing_Club.Controllers
{
    public class ClubController : Controller
    {
        // Dep. Injection
        private readonly IClubRepository _clubRepository;
        private readonly IPhotosService _photoService;

        public ClubController(IClubRepository clubRepository, IPhotosService photoService) // Refactoring to use the Repo. Pattern
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
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
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid) // Will valid 
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(), // We will store the URL into the DB
                    Address = new Address()
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State
                    }
                };

                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to upload photo");
            }

            return View(clubVM);
        }
    }
}