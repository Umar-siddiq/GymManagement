using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Data.Models;

namespace GymManagement.Data.IRepository
{
	public interface IGymRepository : IRepository<Gym>
	{
        void Update(Gym gym);
    }
}
