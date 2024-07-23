
using System.Linq.Expressions;

namespace GymManagement.Data.IRepository
{
	public interface IRepository<T> where T : class
	{
		//T  = Category

		IEnumerable<T> GetAll(string? includeproperties = null); 

		T Get(Expression<Func<T, bool>> filter, string? includeproperties = null);
		void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entity);
	}
}
