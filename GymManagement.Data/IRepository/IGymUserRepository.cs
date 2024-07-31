using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Data.Models;

namespace GymManagement.Data.IRepository
{
	public interface IGymUserRepository : IRepository<GymUser>
	{
        void Update(GymUser gym);
    }
}
