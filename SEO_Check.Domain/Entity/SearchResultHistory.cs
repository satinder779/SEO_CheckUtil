using System;

namespace SEO_Check.Domain.Entity
{
	public class SearchResultHistory
	{
		private SearchEngineInfo _searchEngineInfo;
		public Guid Id { get; set; }
		public string TargetUrl { get; set; }
		public string Keyword { get; set; }
		public long PageRank { get; set; }
		public DateTime DateCreated { get; set; }
		public Guid SearchEngineId { get; set; }

		public virtual SearchEngineInfo SearchEngineInfo
		{
			get { return _searchEngineInfo; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				_searchEngineInfo = value;
				SearchEngineId = value.Id;
			}
		}
	}
}
