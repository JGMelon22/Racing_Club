using Racing_Club.ViewModels;

namespace Racing_Club.Controllers
{
    public class RaceController : Controller
    {
        // Dep. Injection
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotosService _photoService;

        public RaceController(IRaceRepository raceRepository, IPhotosService photoService)
        {
            _raceRepository = raceRepository;
            _photoService = photoService;
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
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);
                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address()
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State
                    }
                };

                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }

            else
            {
                ModelState.AddModelError("", "Failed to upload photo");
            }

            return View(raceVM);
        }
    }
}