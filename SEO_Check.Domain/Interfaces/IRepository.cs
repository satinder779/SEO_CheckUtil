using System.Collections.Generic;

namespace SEO_Check.Domain.Interfaces
{
	public interface IRepository<TEntity> where TEntity : class
	{
		List<TEntity> GetAll();
		TEntity FindById(object id);
		void Add(TEntity entity);
		void Update(TEntity entity);
		void Remove(TEntity entity);
	}
}
