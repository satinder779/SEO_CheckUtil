using SEO_Check.Domain.Entity;
using SEO_Check.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEO_Check.Persistance.Repositories
{
	internal class SearchResultHistoryRepository : Repository<SearchResultHistory>, ISearchResultHistoryRepository
	{
		public SearchResultHistoryRepository(ApplicationDbContext context) : base(context)
		{
		}

		public void AddSearchResultHistory(SearchResultHistory _searchResults)
		{
			Add(_searchResults);
		}

		public IQueryable<SearchResultHistory> GetAllSearchResultHistory()
		{
			return GetAll()
				.AsQueryable();
		}
	}
}
