using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEO_Check.Web.Models
{

	public class SearchEngineResultsModel
	{
		public List<SearchEngineResult> _googleSearchEngineResult { get; set; }
		public List<SearchEngineResult> _yahooSearchEngineResult { get; set; }
		public List<SearchEngineResult> _bingSearchEngineResult { get; set; }
	}

	public class SearchEngineResult
	{
		public string TargetUrl { get; set; }

		[MaxLength(500)]
		public string Keyword { get; set; }
		public long PageRank { get; set; }

		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime DateCreated { get; set; }
		public string SearchEngineName { get; set; }
	}
}
