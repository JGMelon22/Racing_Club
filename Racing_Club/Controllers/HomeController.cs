using System.Diagnostics;
using System.Globalization;
using System.Net;
using Newtonsoft.Json;

namespace Racing_Club.Controllers;

public class HomeController : Controller
{
    private readonly IClubRepository _clubRepository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IClubRepository clubRepository)
    {
        _logger = logger;
        _clubRepository = clubRepository;
    }

    // GEO Location is not been display at any view.
    public async Task<IActionResult> Index()
    {
        var ipInfo = new IPInfo();
        var homeViewModel = new HomeViewModel();

        try
        {
            var url = "https://ipinfo.io?token=xxxxxxxxxx"; // Here you will put you token
            var info = new WebClient().DownloadString("url");
            ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
            var myRI1 = new RegionInfo(ipInfo.Country);
            ipInfo.Country = myRI1.EnglishName;
            homeViewModel.City = ipInfo.City;
            homeViewModel.State = ipInfo.Region;

            // Will get the user location and hit the db
            if (homeViewModel.City != null)
                homeViewModel.Clubs = await _clubRepository.GetClubByCity(homeViewModel.City);

            else
                homeViewModel.Clubs = null;

            return View(homeViewModel);
        }

        // Any network or whatever error, will just null the clubs values
        catch (Exception e)
        {
            homeViewModel.Clubs = null;
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}