namespace Racing_Club.Controllers
{
    public class ClubController : Controller
    {
        // Dep. Injection
        private readonly IClubRepository _clubRepository;
        private readonly IPhotosService _photoService;

        public ClubController(IClubRepository clubRepository,
            IPhotosService photoService) // Refactoring to use the Repo. Pattern
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

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);

            if (club == null)
                return View("Error");

            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                URL = club.Image,
                ClubCategory = club.ClubCategory
            };

            return View(clubVM);
        }

        // Edit Post
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit Club");
                return View("Edit");
            }

            // Will get data from DB if works the previous step
            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);

            if (userClub != null)
                try
                {
                    await _photoService.DeletePhotoAsync(userClub.Image); // Deletes the previous picture
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Could not Delete Photo");
                    return View(clubVM);
                }

            // Been valid, will add the new photo
            var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);

            var club = new Club
            {
                Id = id,
                Title = clubVM.Title,
                Description = clubVM.Description,
                Image = photoResult.Url.ToString(),
                AddressId = clubVM.AddressId,
                Address = clubVM.Address
            };

            // Update the infos inside the DB 
            _clubRepository.Update(club);

            return RedirectToAction("Index");
        }
    }
}