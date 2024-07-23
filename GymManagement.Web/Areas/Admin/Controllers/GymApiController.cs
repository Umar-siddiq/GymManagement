using GymManagement.DataAccess;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Data.Models;
using System.Linq;
using GymManagement.Utility.Services;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace GymManagement.Web.Areas.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class GymApiController : Controller
    {
        private readonly GymDbContext _db;
        private readonly GymApiService _api;

        public GymApiController(GymApiService api, GymDbContext db)
        {
            _db = db;
            _api = api;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Gym>> GetGyms()
        {
			return _db.Gyms.ToList();
        }

        [HttpGet]
        public ActionResult<Gym> GetGym(int id)
        {
            var gym = _db.Gyms.Find(id);

            if (gym == null)
                return NotFound();
            

            return gym;
        }

        [HttpPost]
        public async Task<ActionResult> Postgym(Gym gym)
        {
            _db.Gyms.Add(gym);
			await _db.SaveChangesAsync();


			return CreatedAtAction(nameof(GetGym), new {id=gym.Id}, gym);
        }

        [HttpPut]
        public async Task<IActionResult> PutGym(int id, Gym gym) 
        {
            if( id != gym.Id) 
                return BadRequest();

            _db.Entry(gym).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGym(int id) 
        {
            var gym = await _db.Gyms.FindAsync(id);

            if (gym == null)
                return NotFound();
 
            _db.Gyms.Remove(gym);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
