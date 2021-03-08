using Microsoft.EntityFrameworkCore;
using SEO_Check.Domain;
using SEO_Check.Domain.Interfaces;
using SEO_Check.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEO_Check.Persistance
{
	public class UnitOfWork : IUnitOfWork
	{
		#region Fields
		private readonly ApplicationDbContext _context;
		private IGoogleSearch _googleSearch;
		private ISearchEngineInfoRepository _searchEngineInfoRepository;
		private ISearchParamRepository _searchParamRepository;
		private ISearchResultHistoryRepository _searchResultHistoryRepository;
		private bool _disposed;
		#endregion


		public UnitOfWork(DbContextOptions<ApplicationDbContext> options)
		{
			_context = new ApplicationDbContext(options);
		}

		#region IUnitOfWork Members
		public IGoogleSearch GoogleSearch
		{
			get
			{
				return _googleSearch
					?? (_googleSearch = new GoogleSearch());
			}
		}
		public ISearchEngineInfoRepository SearchEngineInfoRepository
		{
			get
			{
				return _searchEngineInfoRepository
					?? (_searchEngineInfoRepository = new SearchEngineInfoRepository(_context));
			}
		}

		public ISearchParamRepository SearchParamRepository
		{
			get
			{
				return _searchParamRepository
					?? (_searchParamRepository = new SearchParamRepository(_context));
			}
		}

		public ISearchResultHistoryRepository SearchResultHistoryRepository
		{
			get
			{
				return _searchResultHistoryRepository
					?? (_searchResultHistoryRepository = new SearchResultHistoryRepository(_context));
			}
		}

		public void Dispose()
		{
			dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

		#region Private Methods
		private void resetRepositories()
		{
			_googleSearch = null;
		}

		private void dispose(bool disposing)
		{
			if (!_disposed)
			{
				_disposed = true;
			}
		}
		~UnitOfWork()
		{
			dispose(false);
		}
		#endregion
	}
}
