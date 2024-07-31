using GymManagement.DataAccess;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Data.Models;
using GymManagement.Utility.Services;
using Microsoft.EntityFrameworkCore;


namespace GymManagement.Web.Areas.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class GymUserApiController : Controller
    {
        private readonly GymDbContext _db;
        private readonly GymApiService _api;

        public GymUserApiController(GymApiService api, GymDbContext db)
        {
            _db = db;
            _api = api;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GymUser>> GetGymUsers()
        {
            return _db.GymUsers.ToList();
        }

        [HttpGet]
        public ActionResult<GymUser> GetGymUsers(int id)
        {
            var gymUser = _db.GymUsers.Find(id);

            if (gymUser == null)
                return NotFound();


            return gymUser;
        }

        [HttpPost]
        public async Task<ActionResult> PostgymUser(GymUser gymUser)
        {
			gymUser.UserName = gymUser.Email;
			gymUser.NormalizedUserName = gymUser.UserName.Normalize();
			gymUser.NormalizedEmail = gymUser.Email.Normalize();



			_db.GymUsers.Add(gymUser);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGymUsers), new { id = gymUser.Id }, gymUser);
        }

        [HttpGet("{Search}")]
        public async Task<ActionResult> Search(string query)
        {
            var gymUser = await _db.GymUsers.Where(x => x.Full_Name.Contains(query)).ToListAsync();



            return Ok(gymUser);
        }

        public async Task<IActionResult> PutGym(int id, Gym gym)
        {
            if (id != gym.Id)
                return BadRequest();

            _db.Entry(gym).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGymUser(string id)
        {
            var gymUser = await _db.GymUsers.FindAsync(id);

            if (gymUser == null)
                return NotFound();

            _db.GymUsers.Remove(gymUser);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGymUser(string id, [FromBody] GymUser gymUser)
        {
            var existingGym = await _db.GymUsers.FindAsync(id);

            if (existingGym == null)
            {
                return NotFound("Gym Not Found");
            }

            existingGym.UserName= gymUser.Email;
            existingGym.NormalizedUserName = existingGym.UserName.Normalize();
            existingGym.Email = gymUser.Email;
            existingGym.NormalizedEmail = gymUser.Email.Normalize();
			existingGym.Role = gymUser.Role;
			existingGym.City = gymUser.City;
            existingGym.Age = gymUser.Age;
            existingGym.Weight = gymUser.Weight;
            existingGym.Height = gymUser.Height;
            existingGym.Membership = gymUser.Membership;
            existingGym.PhoneNumber = gymUser.PhoneNumber;
            existingGym.Full_Name = gymUser.Full_Name;
            existingGym.Gender = gymUser.Gender;
            existingGym.Type = gymUser.Type;

            try
            {
                await _db.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!_db.GymUsers.Any(e => e.Id == Convert.ToString(id)))
                    return NotFound();

                else
                    throw;
            }

            return NoContent();
        }

    }
}
