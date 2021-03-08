using SEO_Check.Domain.Entity;
using System;
using System.Linq;

namespace SEO_Check.Domain.Interfaces
{
	public interface ISearchEngineInfoRepository
	{
		IQueryable<SearchEngineInfo> GetAllSearchEngines();
		void AddSearchEngine(SearchEngineInfo _searchEngineInfo);

		IQueryable<SearchEngineInfo> FindById(Guid Id);

	}
}
