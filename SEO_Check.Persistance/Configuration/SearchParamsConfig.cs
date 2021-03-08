using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SEO_Check.Domain.Entity;
using System;

namespace SEO_Check.Persistance.Configuration
{
	public class SearchParamsConfig : IEntityTypeConfiguration<SearchParams>
	{
		public void Configure(EntityTypeBuilder<SearchParams> builder)
		{
			builder.ToTable("SearchParams");

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

			builder.Property(x => x.DateCreated)
				 .HasColumnName("DateCreated")
				 .HasColumnType("datetime")
				 .HasDefaultValue(DateTime.Now)
				.IsRequired(true);
		}
	}
}
