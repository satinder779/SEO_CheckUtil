
using SEO_Check.Domain.Entity;
using System.Linq;

namespace SEO_Check.Persistance
{
	public static class DbInitializer
	{
		public static void Initialize(ApplicationDbContext context)
		{
			context.Database.EnsureCreated();

			// Look for any students.
			if (context.SearchEngineInfo.Any())
			{
				return;   // DB has been seeded
			}

			var _searchEngineInfo = new SearchEngineInfo[]
			{
			new SearchEngineInfo{SearchEngineName="Google", ResultsCount=100},
			new SearchEngineInfo{SearchEngineName="Bing", ResultsCount=100},
			new SearchEngineInfo{SearchEngineName="Yahoo", ResultsCount=100}
			};
			foreach (SearchEngineInfo s in _searchEngineInfo)
			{
				context.SearchEngineInfo.Add(s);
			}
			context.SaveChanges();
		}
	}
}
