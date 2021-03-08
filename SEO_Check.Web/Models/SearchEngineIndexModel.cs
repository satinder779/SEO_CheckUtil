using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEO_Check.Web.Models
{

	public class SearchEngineIndexModel
	{
		public SearchEngineIndexModel()
		{
			listSearchEngines = new List<SearchEngineInfoModel>();
			searchParams = new SearchParamsModel();
		}
		public List<SearchEngineInfoModel> listSearchEngines { get; set; }
		public SearchParamsModel searchParams { get; set; }
	}
	public class SearchEngineInfoModel
	{
		public Guid Id { get; set; }

		[Display(Name = "Search Engine Name")]
		public string SearchEngineName { get; set; }

		[Display(Name = "Results Count")]
		public long ResultsCount { get; set; }
		public bool Selected { get; set; }
	}
	public class SearchParamsModel
	{
		public Guid Id { get; set; }

		[MaxLength(500)]
		[Required]
		[Display(Name = "Keyword")]
		public string Keyword { get; set; }

		[Required]
		[Display(Name = "Target Url")]
		public string TargetUrl { get; set; }
		public long ResultsCount { get; set; }
	}
}
