using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SEO_Check.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEO_Check.Persistance.Configuration
{
	public class SearchResultHistoryConfig : IEntityTypeConfiguration<SearchResultHistory>
	{
		public void Configure(EntityTypeBuilder<SearchResultHistory> builder)
		{
			builder.ToTable("SearchResultHistory");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
			   .HasColumnName("Id")
			   .HasColumnType("uniqueidentifier")
			   .IsRequired();

			builder.Property(x => x.Keyword)
				.HasColumnName("Keyword")
				.HasColumnType("nvarchar")
				.HasMaxLength(500)
			   .IsRequired(false);

			builder.Property(x => x.TargetUrl)
				 .HasColumnName("TargetUrl")
				 .HasColumnType("nvarchar")
				 .HasMaxLength(250)
				 .IsFixedLength()
			   .IsRequired(false);

			builder.Property(x => x.PageRank)
				 .HasColumnName("PageRank")
				 .HasColumnType("bigint")
			   .IsRequired(true);

			builder.Property(x => x.DateCreated)
				 .HasColumnName("DateCreated")
				 .HasColumnType("datetime")
				 .HasDefaultValue(DateTime.Now)
				.IsRequired(true);

			builder.Property(x => x.SearchEngineId)
				.HasColumnName("SearchEngineId")
				.HasColumnType("uniqueidentifier")
				.IsRequired();
			
			builder.HasOne(x => x.SearchEngineInfo)
				.WithMany(x => x.SearchResultHistory)
				.HasForeignKey(x => x.SearchEngineId);

			//builder.Navigation(x => x.SearchEngineInfo)
			//	.WithMany(x => x.SearchResultHistory)
			//	.HasForeignKey(x => x.SearchEngineId);
		}
	}
}
