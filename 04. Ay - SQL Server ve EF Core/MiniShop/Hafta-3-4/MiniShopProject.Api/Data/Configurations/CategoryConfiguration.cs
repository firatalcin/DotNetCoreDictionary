using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShopProject.Api.Entities;

namespace MiniShopProject.Api.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        // Sadece silinmemiş kayıtlar arasında unique olsun (silinen ad tekrar kullanılabilsin)
        builder.HasIndex(c => c.Name).IsUnique().HasFilter("[IsDeleted] = 0");

        // Soft delete: Silinmiş kayıtlar hiçbir LINQ sorgusunda gelmesin
        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}