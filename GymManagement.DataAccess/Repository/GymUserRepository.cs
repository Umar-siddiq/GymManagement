using GymManagement.Data.IRepository;
using GymManagement.Data.Models;
using GymManagement.DataAccess;

namespace GymManagement.DataAccess.Repository
{
	public class GymUserRepository : Repository<GymUser>, IGymUserRepository
    {
		private GymDbContext _db;

		public GymUserRepository(GymDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(GymUser gym)
		{
			_db.Update(gym);
		}
	}
}
