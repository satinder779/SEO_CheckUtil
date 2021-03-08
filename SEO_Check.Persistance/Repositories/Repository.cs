using Microsoft.EntityFrameworkCore;
using SEO_Check.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEO_Check.Persistance.Repositories
{
	internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private ApplicationDbContext _context;
		private DbSet<TEntity> _set;

		internal Repository(ApplicationDbContext context)
		{
			_context = context;
		}
		protected DbSet<TEntity> Set
		{
			get { return _set ?? (_set = _context.Set<TEntity>()); }
		}
		public List<TEntity> GetAll()
		{
			return Set.ToList();
		}

		public TEntity FindById(object id)
		{
			return Set.Find(id);
		}

		public void Add(TEntity entity)
		{
			Set.Add(entity);
			_context.SaveChanges();
		}

		public void Update(TEntity entity)
		{
			var entry = _context.Entry(entity);
			if (entry.State == EntityState.Detached)
			{
				Set.Attach(entity);
				entry = _context.Entry(entity);
			}
			entry.State = EntityState.Modified;
		}

		public void Remove(TEntity entity)
		{
			Set.Remove(entity);
		}
	}
}
