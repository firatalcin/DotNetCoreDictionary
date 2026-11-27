using Microsoft.EntityFrameworkCore;
using MiniShopProject.Api.Entities;

namespace MiniShopProject.Api.Data;

public class MiniShopDbContext : DbContext
{
    public MiniShopDbContext(DbContextOptions<MiniShopDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Bu projedeki tüm IEntityTypeConfiguration sınıflarını bulup uygular
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MiniShopDbContext).Assembly);
    }

    // Tüm kayıtlar buradan geçtiği için tarih ve soft delete işini tek noktada yapıyoruz
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        // ToList(): Döngü içinde State değiştireceğimiz için listenin kopyası üzerinde dönüyoruz
        foreach (var entry in ChangeTracker.Entries<BaseEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = now;
                    break;

                case EntityState.Modified:
                    entry.Entity.ModifiedDate = now;
                    break;

                case EntityState.Deleted:
                    // DELETE yerine UPDATE yapılsın (Soft Delete)
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.ModifiedDate = now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}