using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShopProject.Api.Entities;

namespace MiniShopProject.Api.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(c => c.LastName).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(150);


        // Sadece silinmemiş kayıtlar arasında unique olsun (silinen e-posta tekrar kullanılabilsin)
        builder.HasIndex(c => c.Email).IsUnique().HasFilter("[IsDeleted] = 0");

        // Soft delete: Silinmiş kayıtlar hiçbir LINQ sorgusunda gelmesin
        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}