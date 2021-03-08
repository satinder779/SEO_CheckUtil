using Microsoft.EntityFrameworkCore;
using SEO_Check.Domain.Entity;
using SEO_Check.Persistance.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEO_Check.Persistance
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<SearchEngineInfo> SearchEngineInfo { get; set; }
		public DbSet<SearchParams> SearchParams { get; set; }
		public DbSet<SearchResultHistory> SearchResultHistory { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration<SearchEngineInfo>(new SearchEngineInfoConfig());
			modelBuilder.ApplyConfiguration<SearchParams>(new SearchParamsConfig());
			modelBuilder.ApplyConfiguration<SearchResultHistory>(new SearchResultHistoryConfig());
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies();
		}
	}
}
