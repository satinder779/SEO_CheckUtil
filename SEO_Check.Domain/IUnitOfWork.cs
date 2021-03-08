using SEO_Check.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEO_Check.Domain
{
	public interface IUnitOfWork : IDisposable
	{
		IGoogleSearch GoogleSearch { get; }
		ISearchEngineInfoRepository SearchEngineInfoRepository { get; }
		ISearchParamRepository SearchParamRepository { get; }
		ISearchResultHistoryRepository SearchResultHistoryRepository { get; }
	}
}
