using System;
using System.Collections.Generic;
using System.Text;

namespace SEO_Check.Domain.SearchDTO
{
	public class GoogleSearchRequestDTO
	{
		public string Keyword { get; set; }
		public string TargetURL { get; set; }
		public long ResultsCount { get; set; }
	}
}
