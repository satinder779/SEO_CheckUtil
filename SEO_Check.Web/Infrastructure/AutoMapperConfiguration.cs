using AutoMapper;
using SEO_Check.Domain.Entity;
using SEO_Check.Web.Models;

namespace SEO_Check.Web.Infrastructure
{
	public class AutoMapperConfiguration : Profile
	{
		public AutoMapperConfiguration()
		{
			// Default mapping when property names are same
			CreateMap<SearchEngineInfoModel, SearchEngineInfo>();
			CreateMap<SearchEngineInfo, SearchEngineInfoModel>();

			CreateMap<SearchParamsModel, SearchParams>();
			CreateMap<SearchParams, SearchParamsModel>();

			CreateMap<SearchEngineResult, SearchResultHistory>();
			//CreateMap<SearchResultHistory, SearchEngineResultsModel>();


			var map = CreateMap<SearchResultHistory, SearchEngineResult>();
			//// ingnore all existing binding of property
			//map.ForAllMembers(opt => opt.Ignore());
			//// than map property as following
			map.ForMember(dest => dest.SearchEngineName, opt => opt.MapFrom(src => src.SearchEngineInfo.SearchEngineName));
		}
	}
}
