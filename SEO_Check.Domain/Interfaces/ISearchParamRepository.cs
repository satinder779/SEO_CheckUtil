using SEO_Check.Domain.Entity;
using System.Linq;

namespace SEO_Check.Domain.Interfaces
{
	public interface ISearchParamRepository
	{
		IQueryable<SearchParams> GetAllSearchParams();
		void AddSearchEngineParam(SearchParams _searchParams);
	}
}
