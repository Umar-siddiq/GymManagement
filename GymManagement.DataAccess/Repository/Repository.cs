using GymManagement.Data.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GymManagement.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly GymDbContext _db;
		private DbSet<T> dbset;

        public Repository(GymDbContext db)
        {
			_db = db;
            this.dbset = db.Set<T>();
        }

        public void Add(T entity)
		{
			dbset.Add(entity);
		}

       

        public T Get(Expression<Func<T, bool>> filter, string? includeproperties = null)
		{
			IQueryable<T> query = dbset;
			query = query.Where(filter);
			return query.FirstOrDefault();
		}

		public IEnumerable<T> GetAll(string? includeproperties = null)
			{
			IQueryable<T> query = dbset;

            if (!string.IsNullOrEmpty(includeproperties))
            {
                foreach (var includeProperty in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.ToList() ?? new List<T>();
		}

		public void Remove(T entity)
		{
			dbset.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			dbset.RemoveRange(entity);
		}
	}
}
