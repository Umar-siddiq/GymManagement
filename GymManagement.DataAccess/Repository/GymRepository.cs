using GymManagement.Data.IRepository;
using GymManagement.Data.Models;
using GymManagement.DataAccess;

namespace GymManagement.DataAccess.Repository
{
	public class GymRepository : Repository<Gym>, IGymRepository
	{
		private GymDbContext _db;

		public GymRepository(GymDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(Gym gym)
		{
			_db.Update(gym);
		}
	}
}
