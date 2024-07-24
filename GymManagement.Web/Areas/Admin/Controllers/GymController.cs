using Microsoft.AspNetCore.Mvc;
using GymManagement.DataAccess;
using GymManagement.Data.Models;
using GymManagement.Utility.Services;
using GymManagement.Data.IRepository;
using GymManagement.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using GymManagement.DataAccess.Repository;


namespace GymManagement.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GymController : Controller
    {
        private readonly GymDbContext _db;
        private readonly IUnitOfWork _unitofwork;
        private readonly GymApiService _api;

        public GymController(GymApiService api, GymDbContext db, IUnitOfWork unitofwork)
        {
            _db = db;
            _api = api;
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            List<Gym> GymList = _db.Gyms.ToList();

            return View(GymList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Gym gym)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response;

                    response = await _api.CreateGymAsync(gym);


                if (response.IsSuccessStatusCode )
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

        
        public IActionResult Update(int id)
        {
            GymVM gymVM = new();

            if (id == 0)
                return View(gymVM);

            else
            {
                gymVM.Gym = _unitofwork.Gym.Get(u => u.Id == id);
                return View(gymVM);
            }

        }


        [HttpPost]
        public async Task<IActionResult> Update(GymVM gymVM)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response;
                response = await _api.UpdateGymAsync(gymVM.Gym.Id, gymVM.Gym);


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
