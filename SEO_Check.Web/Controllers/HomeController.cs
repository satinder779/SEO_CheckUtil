using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SEO_Check.Domain;
using SEO_Check.Domain.Entity;
using SEO_Check.Web.Models;

namespace SEO_Check.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private IMemoryCache _cache;
		public const string _Google = "GOOGLE";
		public const string _Yahoo = "YAHOO";
		public const string _Bing = "BING";

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_cache = memoryCache;
		}


		public IActionResult Index()
		{
			SearchEngineIndexModel model = new SearchEngineIndexModel();

			var searchEngines = _unitOfWork.SearchEngineInfoRepository.GetAllSearchEngines().ToList();
			model.listSearchEngines = _mapper.Map<List<SearchEngineInfoModel>>(searchEngines);
			var configuredSearchParams = _unitOfWork.SearchParamRepository.GetAllSearchParams().ToList().OrderByDescending(d => d.DateCreated).FirstOrDefault();
			model.searchParams = _mapper.Map<SearchParamsModel>(configuredSearchParams);
			return View(model);
		}

		[HttpPost]
		public IActionResult Index(SearchEngineIndexModel model)
		{
			string selectedSearchEngine = Request.Form["chkSelectedSearchEngine"].ToString();
			string[] selectedIds = selectedSearchEngine.Split(",");
			var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));

			if (!string.IsNullOrEmpty(model.searchParams.Keyword))
			{
				_unitOfWork.SearchParamRepository.AddSearchEngineParam(new SearchParams() { Id = Guid.NewGuid(), TargetUrl = model.searchParams.TargetUrl, Keyword = model.searchParams.Keyword, DateCreated = DateTime.Now });
			}

			foreach (var searchEngineId in selectedIds)
			{
				if (!string.IsNullOrEmpty(searchEngineId))
				{
					var _selectedEngine = _unitOfWork.SearchEngineInfoRepository.FindById(new Guid(searchEngineId)).FirstOrDefault();
					if (_selectedEngine != null)
					{
						switch (_selectedEngine.SearchEngineName.ToUpper())
						{
							case _Google:
								long pageRankGoogle;
								bool isGoogleCacheExist = _cache.TryGetValue(_Google, out pageRankGoogle);
								if (!isGoogleCacheExist)
								{
									pageRankGoogle = _unitOfWork.GoogleSearch.GetPageRank(new Domain.SearchDTO.GoogleSearchRequestDTO() { Keyword = model.searchParams.Keyword, TargetURL = model.searchParams.TargetUrl, ResultsCount = _selectedEngine.ResultsCount });
									_cache.Set(_Google, pageRankGoogle, cacheEntryOptions);


									_unitOfWork.SearchResultHistoryRepository.AddSearchResultHistory(new SearchResultHistory() { Id = Guid.NewGuid(), Keyword = model.searchParams.Keyword, TargetUrl = model.searchParams.TargetUrl, PageRank = pageRankGoogle, DateCreated = DateTime.Now, SearchEngineId = new Guid(searchEngineId) });
								}
								break;
							case _Bing:
								long pageRankBing;
								bool isBingCacheExist = _cache.TryGetValue(_Bing, out pageRankBing);
								if (!isBingCacheExist)
								{
									pageRankBing = _unitOfWork.GoogleSearch.GetPageRank(new Domain.SearchDTO.GoogleSearchRequestDTO() { Keyword = model.searchParams.Keyword, TargetURL = model.searchParams.TargetUrl, ResultsCount = _selectedEngine.ResultsCount });
									_cache.Set(_Bing, pageRankBing, cacheEntryOptions);


									_unitOfWork.SearchResultHistoryRepository.AddSearchResultHistory(new SearchResultHistory() { Id = Guid.NewGuid(), Keyword = model.searchParams.Keyword, TargetUrl = model.searchParams.TargetUrl, PageRank = pageRankBing, DateCreated = DateTime.Now, SearchEngineId = new Guid(searchEngineId) });
								}
								break;
							case _Yahoo:
								long pageRankYahoo;
								bool isYahooCacheExist = _cache.TryGetValue(_Yahoo, out pageRankYahoo);
								if (!isYahooCacheExist)
								{
									pageRankYahoo = _unitOfWork.GoogleSearch.GetPageRank(new Domain.SearchDTO.GoogleSearchRequestDTO() { Keyword = model.searchParams.Keyword, TargetURL = model.searchParams.TargetUrl, ResultsCount = _selectedEngine.ResultsCount });
									_cache.Set(_Yahoo, pageRankYahoo, cacheEntryOptions);


									_unitOfWork.SearchResultHistoryRepository.AddSearchResultHistory(new SearchResultHistory() { Id = Guid.NewGuid(), Keyword = model.searchParams.Keyword, TargetUrl = model.searchParams.TargetUrl, PageRank = pageRankYahoo, DateCreated = DateTime.Now, SearchEngineId = new Guid(searchEngineId) });
								}
								break;
						}
					}
				}
			}

			return RedirectToAction("Results");
		}

		public IActionResult Results()
		{
			SearchEngineResultsModel model = new SearchEngineResultsModel();

			model._googleSearchEngineResult = _mapper.Map<List<SearchEngineResult>>(_unitOfWork.SearchResultHistoryRepository.GetAllSearchResultHistory().Where(n => n.SearchEngineInfo.SearchEngineName.ToUpper() == _Google).ToList());

			model._yahooSearchEngineResult = _mapper.Map<List<SearchEngineResult>>(_unitOfWork.SearchResultHistoryRepository.GetAllSearchResultHistory().Where(n => n.SearchEngineInfo.SearchEngineName.ToUpper() == _Yahoo).OrderByDescending(n => n.DateCreated).Take(10).ToList());

			model._bingSearchEngineResult = _mapper.Map<List<SearchEngineResult>>(_unitOfWork.SearchResultHistoryRepository.GetAllSearchResultHistory().Where(n => n.SearchEngineInfo.SearchEngineName.ToUpper() == _Bing).OrderByDescending(n => n.DateCreated).Take(10).ToList());

			return View(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
