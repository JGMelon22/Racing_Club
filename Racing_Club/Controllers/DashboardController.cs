using CloudinaryDotNet.Actions;

namespace Racing_Club.Controllers;

public class DashboardController : Controller
{
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPhotoService _photoService;

    public DashboardController(IDashboardRepository dashboardRepository,
        IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
    {
        _dashboardRepository = dashboardRepository;
        _httpContextAccessor = httpContextAccessor;
        _photoService = photoService;
    }

    // Stupid simple auto mapper to avoid tracking errors
    private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVM, ImageUploadResult photoResult)
    {
        user.Id = editVM.Id;
        user.Pace = editVM.Pace;
        user.Mileage = editVM.Mileage;
        user.ProfileImageUrl =
            photoResult.Url
                .ToString(); // Cloudinary will be the responsible to generate the url to us, we will only convert to string
        user.City = editVM.City;
        user.State = editVM.State;
    }

    public async Task<IActionResult> Index()
    {
        // Specific data to be return from the DB for each user
        var userRaces = await _dashboardRepository.GetAllUserRaces();
        var userClubs = await _dashboardRepository.GetAllUserClubs();

        // All in one package thanks to the view model :)
        var dashboardViewModel = new DashboardViewModel
        {
            Races = userRaces,
            Clubs = userClubs
        };

        return View(dashboardViewModel);
    }

    // Edit User Profile
    public async Task<IActionResult> EditUserProfile()
    {
        var curUserId = _httpContextAccessor.HttpContext.User.GetUserId(); // Neat extension method btw
        /* AppUser */
        var user = await _dashboardRepository.GetUserById(curUserId);

        // Just in case if random error happen, will be redirect to an user friendly view
        if (user == null)
            return View("Error");

        var editUserViewModel = new EditUserDashboardViewModel
        {
            Id = curUserId,
            Pace = user.Pace,
            Mileage = user.Mileage,
            ProfileImageUrl = user.ProfileImageUrl, // This 3 items will be hardcoded at first
            City = user.City,
            State = user.State
        };

        return View(editUserViewModel);
    }

    // Post info from the form
    [HttpPost]
    public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to Edit Profile");
            return View("EditUserProfile", editVM);
        }

        var user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);

        if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
        {
            var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

            // Optimistic Concurrency 
            MapUserEdit(user, editVM, photoResult);

            _dashboardRepository.Update(user);

            return RedirectToAction("Index");
        }

        else
        {
            try
            {
                await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Could not delete photo");
                return View(editVM);
            }

            // If all works, will delete and replace the user photo with a new one (or a blank)
            var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

            MapUserEdit(user, editVM, photoResult);

            _dashboardRepository.Update(user);

            return RedirectToAction("Index");
        }
    }
}