using SEO_Check.Domain.Entity;
using SEO_Check.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEO_Check.Persistance.Repositories
{
	internal class SearchEngineInfoRepository : Repository<SearchEngineInfo>, ISearchEngineInfoRepository
	{

		public SearchEngineInfoRepository(ApplicationDbContext context) : base(context)
		{
		}

		public void AddSearchEngine(SearchEngineInfo _searchEngineInfo)
		{
			Add(_searchEngineInfo);
		}

		public IQueryable<SearchEngineInfo> FindById(Guid Id)
		{
			return Set.Where(x => x.Id == Id).AsQueryable();
		}

		public IQueryable<SearchEngineInfo> GetAllSearchEngines()
		{
			return GetAll()
				.AsQueryable();
		}
	}
}
