using GymManagement.Data.IRepository;
using GymManagement.Data.Models;
using GymManagement.Data.ViewModels;
using GymManagement.DataAccess;
using GymManagement.Utility.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GymManagement.Web.Areas.Admin.Controllers
{

    //[Authorize(Roles = Roles.Role_Admin)]
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
            IEnumerable<GymUser> gymUser = _db.GymUsers.ToList();//.Select(user => new GymUserViewModel
            //{
            //    Id = user.Id, 
            //    Full_Name = user.Full_Name,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber,
            //    Type = user.Type,
            //    Role = ResolveRole(user.Role)),
            //    us


            //}).ToList();

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

        public IActionResult Update(string id)
        {
            GymUser gymUser = new();

            if (id == null)
                return View(gymUser);

            else
            {
                gymUser = _unitofwork.GymUser.Get(u => u.Id == id);
                return View(gymUser);
            }

        }


        [HttpPost]
        public async Task<IActionResult> Update(GymUser gymUser)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response;
                response = await _api.UpdateGymUserAsync(gymUser.Id, gymUser);


                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Occured");
                }
            }

            return View();
        }






        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _api.DeleteGymUserAsync(id);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            else ModelState.AddModelError(string.Empty, "An Error Occured");

            return RedirectToAction(nameof(Index));
        }
    }
}

