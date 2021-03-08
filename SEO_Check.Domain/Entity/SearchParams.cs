using System;

namespace SEO_Check.Domain.Entity
{
	public class SearchParams
	{
		public Guid Id { get; set; }
		public string Keyword { get; set; }
		public string TargetUrl { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
