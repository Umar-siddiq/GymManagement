using GymManagement.Data.IRepository;
using GymManagement.Data.Models;
using GymManagement.Data.ViewModels;
using GymManagement.DataAccess;
using GymManagement.Utility;
using GymManagement.Utility.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GymManagement.Web.Areas.Admin.Controllers
{

    //[Authorize(Roles = Roles.Role_Admin)]
    [Area("Admin")]
    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;
        private readonly GymDbContext _db;
        private readonly IUnitOfWork _unitofwork;
        private readonly GymUserApiService _api;

        private readonly ILogger<GymController> _logger;

        public UserController(GymUserApiService api, GymDbContext db, IUnitOfWork unitofwork, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _api = api;
            _unitofwork = unitofwork;
            _userManager = userManager;
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

                var existingUser = await _userManager.FindByEmailAsync(gymUser.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "User already exists.");
                    return View(gymUser);
                }



                if (!string.IsNullOrEmpty(gymUser.Role))
                {
                    switch (gymUser.Role) {
                        case "Admin":
                            await _userManager.AddToRoleAsync(gymUser, Roles.Role_Admin);
                            break;

                        case "Customer":
                            await _userManager.AddToRoleAsync(gymUser, Roles.Role_Customer);
                            break;

                        case "Employee":
                            await _userManager.AddToRoleAsync(gymUser, Roles.Role_Employee);
                            break;

                        case "Member":
                            await _userManager.AddToRoleAsync(gymUser, Roles.Role_Member);
                            break;

                        case "Company":
                            await _userManager.AddToRoleAsync(gymUser, Roles.Role_Company);
                            break;

                        case "Trainer":
                            await _userManager.AddToRoleAsync(gymUser, Roles.Role_Trainer);
                            break;
                    }
                }
                else
                    await _userManager.AddToRoleAsync(gymUser, Roles.Role_Customer);


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
                // Fetch the user from the database
                var existingUser = await _userManager.FindByIdAsync(gymUser.Id);

                if (existingUser == null)
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                    return View(gymUser);
                }

                HttpResponseMessage response;

                if (!string.IsNullOrEmpty(gymUser.Role))
                {
                    // Remove the user from all current roles
                    var currentRoles = await _userManager.GetRolesAsync(existingUser);
                    await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);

                    // Add the user to the new role
                    switch (gymUser.Role)
                    {
                        case "Admin":
                            await _userManager.AddToRoleAsync(existingUser, Roles.Role_Admin);
                            break;

                        case "Customer":
                            await _userManager.AddToRoleAsync(existingUser, Roles.Role_Customer);
                            break;

                        case "Employee":
                            await _userManager.AddToRoleAsync(existingUser, Roles.Role_Employee);
                            break;

                        case "Member":
                            await _userManager.AddToRoleAsync(existingUser, Roles.Role_Member);
                            break;

                        case "Company":
                            await _userManager.AddToRoleAsync(existingUser, Roles.Role_Company);
                            break;

                        case "Trainer":
                            await _userManager.AddToRoleAsync(existingUser, Roles.Role_Trainer);
                            break;
                        default:
                            await _userManager.AddToRoleAsync(existingUser, Roles.Role_Customer);
                            break;
                    }
                }
                else
                {
                    // Default role assignment if role is not specified
                    await _userManager.AddToRoleAsync(existingUser, Roles.Role_Customer);
                }

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

