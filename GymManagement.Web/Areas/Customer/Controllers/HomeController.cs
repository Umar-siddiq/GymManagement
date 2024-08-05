using GymManagement.Data.IRepository;
using GymManagement.Data.Models;
using GymManagement.Data.ViewModels;
using GymManagement.DataAccess;
using GymManagement.Utility;
using GymManagement.Utility.Services;
using GymManagement.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymManagement.Web.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        UserManager<IdentityUser> _userManager;
        private readonly GymDbContext _db;
        private readonly IUnitOfWork _unitofwork;
        private readonly GymUserApiService _api;
        private readonly ILogger<GymController> _logger;

        public HomeController(GymUserApiService api, GymDbContext db, IUnitOfWork unitofwork, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _api = api;
            _unitofwork = unitofwork;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var gymUser = await _userManager.GetUserAsync(User);

            if (gymUser == null)
                return RedirectToAction("Login", "Account");


            return View(gymUser);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Update( GymUserVM gymUserVM)
        {

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
