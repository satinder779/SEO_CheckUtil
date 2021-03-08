using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SEO_Check.Domain.Entity;

namespace SEO_Check.Persistance.Configuration
{
	public class SearchEngineInfoConfig : IEntityTypeConfiguration<SearchEngineInfo>
	{
		public void Configure(EntityTypeBuilder<SearchEngineInfo> builder)
		{
			builder.ToTable("SearchEngineInfo");

			builder.Property(x => x.Id)
				.HasColumnName("Id")
				.HasColumnType("uniqueidentifier")
				.IsRequired();

			builder.Property(x => x.SearchEngineName)
				.HasColumnName("SearchEngineName")
				.HasColumnType("nvarchar")
				 .HasMaxLength(50)
				 .IsFixedLength()
				 .IsRequired(true);

			builder.Property(x => x.ResultsCount)
				 .HasColumnName("ResultsCount")
				 .HasColumnType("bigint")
				 .IsRequired(true);
					   

			builder.HasMany(x => x.SearchResultHistory)
				.WithOne(x => x.SearchEngineInfo)
				.HasForeignKey(x => x.SearchEngineId);
		}
	}
}
