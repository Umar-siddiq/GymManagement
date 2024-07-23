using Microsoft.AspNetCore.Mvc;
using GymManagement.DataAccess;
using GymManagement.Data.Models;
using GymManagement.Utility.Services;

namespace GymManagement.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GymController : Controller
    {
        private readonly GymDbContext _db;
        private readonly GymApiService _api;

        public GymController(GymApiService api, GymDbContext db)
        {
            _db = db;
            _api = api;
        }

        public IActionResult Index()
        {
            List<Gym> GymList = _db.Gyms.ToList();

            return View(GymList);
        }

        public IActionResult Upsert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Gym gym)
        {
            if (ModelState.IsValid)
            {
                var response = await _api.CreateGymAsync(gym);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Occured");
                }
            }
            return View(gym);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
           
                var response = await _api.DeleteGymAsync(id);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                else ModelState.AddModelError(string.Empty, "An Error Occured");
           
            return RedirectToAction(nameof(Index));
        }

    }
}
