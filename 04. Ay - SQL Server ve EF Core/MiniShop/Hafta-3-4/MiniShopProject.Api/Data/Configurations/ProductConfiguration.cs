using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShopProject.Api.Entities;

namespace MiniShopProject.Api.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // CHECK kısıtları tabloya ait olduğu için ToTable içinde tanımlanır
        builder.ToTable("Products", t =>
        {
            t.HasCheckConstraint("CK_Products_Price", "[Price] >= 0");
            t.HasCheckConstraint("CK_Products_Stock", "[Stock] >= 0");
        });

        builder.HasKey(p => p.Id);
        builder.HasQueryFilter(p => !p.IsDeleted); // Soft delete filtresi

        // SQL Server'da "rowversion" kolonu; UPDATE'lerde WHERE RowVersion = @eski kontrolü eklenir
        builder.Property(p => p.RowVersion).IsRowVersion();

        builder.Property(p => p.Name).IsRequired().HasMaxLength(150);
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Stock).HasDefaultValue(0);

        // Product N -> 1 Category
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // İçinde ürün olan kategori silinemesin

        // Hafta-2'deki INCLUDE'lu index'in aynısı
        builder.HasIndex(p => p.Name)
            .IncludeProperties(p => new { p.Price, p.Stock })
            .HasDatabaseName("IX_Products_Name");
    }
}