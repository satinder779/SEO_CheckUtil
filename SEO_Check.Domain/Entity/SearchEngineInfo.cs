using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEO_Check.Domain.Entity
{
	public class SearchEngineInfo
	{
		private ICollection<SearchResultHistory> _searchResultHistory;

		public Guid Id { get; set; }
		public string SearchEngineName { get; set; }

		[Column(TypeName = "bigint")]
		public long ResultsCount { get; set; }

		public virtual ICollection<SearchResultHistory> SearchResultHistory
		{
			get { return _searchResultHistory ?? (_searchResultHistory = new List<SearchResultHistory>()); }
			set { _searchResultHistory = value; }
		}
	}
}
