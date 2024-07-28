using GymManagement.Data.IRepository;
using GymManagement.Data.Models;
using GymManagement.Data.ViewModels;
using GymManagement.DataAccess;
using GymManagement.Utility.Services;
using GymManagement.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GymManagement.Web.Areas.Admin.Controllers
{

    [Authorize(Roles = Roles.Role_Admin)]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly GymDbContext _db;
        private readonly IUnitOfWork _unitofwork;
        private readonly GymUserApiService _api;

        private readonly ILogger<GymController> _logger;

        public UserController(GymUserApiService api, GymDbContext db, IUnitOfWork unitofwork)
        {
            _db = db;
            _api = api;
            _unitofwork = unitofwork;
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GymUser gymUser)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response;

                response = await _api.CreateGymUserAsync(gymUser);


                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Occured");
                }
            }
            return View(gymUser);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            IEnumerable<GymUser> gymUser = _db.GymUsers.ToList();

            if (!string.IsNullOrEmpty(search))
            {
                var response = await _api.SearchGymUserAsync(search);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    gymUser = JsonConvert.DeserializeObject<IEnumerable<GymUser>>(jsonString);

                    //System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Gym>>
                    //(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            else
            {
            }
            return View(gymUser);
        }

        [HttpGet]
        public IActionResult Search(string search)
        {
            return RedirectToAction("Index", new { search });
        }

        //public IActionResult Update(int id)
        //{
        //	GymVM gymVM = new();

        //	if (id == 0)
        //		return View(gymVM);

        //	else
        //	{
        //		gymVM.Gym = _unitofwork.Gym.Get(u => u.Id == id);
        //		return View(gymVM);
        //	}

        //}


        [HttpPost]
        public async Task<IActionResult> Update(GymVM gymVM)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response;
                //response = await _api.UpdateGymAsync(gymVM.Gym.Id, gymVM.Gym);


                //if (response.IsSuccessStatusCode)
                //{
                //	return RedirectToAction(nameof(Index));
                //}

                //else
                //{
                //	ModelState.AddModelError(string.Empty, "An Error Occured");
                //}
            }



            return View();
        }






        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _api.DeleteGymUserAsync(id);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            else ModelState.AddModelError(string.Empty, "An Error Occured");

            return RedirectToAction(nameof(Index));
        }
    }
}

