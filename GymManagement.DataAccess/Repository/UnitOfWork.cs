using GymManagement.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private GymDbContext _db;
        public IGymRepository Gym { get; private set; }

        public IGymUserRepository GymUser { get; private set; }


        public UnitOfWork(GymDbContext db) 
        {
            _db = db;
            Gym = new GymRepository(_db);
            GymUser = new GymUserRepository(_db);
        }

        public void Save() 
        {
            _db.SaveChanges();
        }
    }
}
