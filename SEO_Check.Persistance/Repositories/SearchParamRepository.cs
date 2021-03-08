using SEO_Check.Domain.Entity;
using SEO_Check.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEO_Check.Persistance.Repositories
{
	internal class SearchParamRepository : Repository<SearchParams>, ISearchParamRepository
	{

		public SearchParamRepository(ApplicationDbContext context) : base(context)
		{
		}
		public void AddSearchEngineParam(SearchParams _searchParams)
		{
			Add(_searchParams);
		}

		public IQueryable<SearchParams> GetAllSearchParams()
		{
			return GetAll()
				.AsQueryable();
		}
	}
}
