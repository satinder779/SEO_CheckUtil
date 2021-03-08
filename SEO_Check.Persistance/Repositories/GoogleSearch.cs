using HtmlAgilityPack;
using NLog;
using SEO_Check.Domain.Interfaces;
using SEO_Check.Domain.SearchDTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SEO_Check.Persistance.Repositories
{
	internal class GoogleSearch : IGoogleSearch
	{
		private readonly string SearchEngineBaseURL = "http://google.com/search?";
		public int GetPageRank(GoogleSearchRequestDTO serachRequest)
		{
			try
			{
				using var client = new WebClient();
				string url = string.Format("{0}num={1}&q={2}", SearchEngineBaseURL, serachRequest.ResultsCount, serachRequest.Keyword.Trim());
				string content = client.DownloadString(url);

				HtmlDocument html = new HtmlDocument();
				html.LoadHtml(content);

				HtmlNode doc = html.DocumentNode;
				List<string> linkLists = new List<string>();

				foreach (HtmlNode link in doc.SelectNodes("//a[@href]"))
				{
					string hrefValue = link.GetAttributeValue("href", string.Empty);
					if (!hrefValue.ToString().ToUpper().Contains("GOOGLE") && hrefValue.ToString().Contains("/url?q=") && (hrefValue.ToString().ToUpper().Contains("HTTP://") || hrefValue.ToString().ToUpper().Contains("HTTPS://")))
					{
						int index = hrefValue.IndexOf("&");
						if (index > 0)
						{
							hrefValue = hrefValue.Substring(0, index);
							linkLists.Add(hrefValue.Replace("/url?q=", ""));
						}
					}
				}

				int linkRank = linkLists.FindIndex(a => a.StartsWith(serachRequest.TargetURL));
				return linkRank + 1;
			}
			catch (Exception ex)
			{
				LogManager.GetCurrentClassLogger().Error(ex, string.Format(" Message : {0} | Stack Trace : {1}.", ex.Message, ex.StackTrace));
				return 0;
			}
		}
	}
}
