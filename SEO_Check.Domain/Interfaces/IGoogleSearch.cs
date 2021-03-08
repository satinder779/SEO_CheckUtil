using SEO_Check.Domain.SearchDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEO_Check.Domain.Interfaces
{
	public interface IGoogleSearch
	{
		int GetPageRank(GoogleSearchRequestDTO serachRequest);
	}
}
