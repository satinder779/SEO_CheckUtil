using SEO_Check.Domain.Entity;
using System.Linq;

namespace SEO_Check.Domain.Interfaces
{
	public interface ISearchResultHistoryRepository
	{
		IQueryable<SearchResultHistory> GetAllSearchResultHistory();
		void AddSearchResultHistory(SearchResultHistory _searchResults);
	}
}
