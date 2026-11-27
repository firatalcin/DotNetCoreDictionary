namespace MiniShopProject.Api.Entities;

public class Product : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public int CategoryId { get; set; }              // Foreign key kolonu
    public Category Category { get; set; } = null!;  // Navigation

    public byte[] RowVersion { get; set; } = null!;  // Değerini SQL Server verir, her UPDATE'te değişir

    public List<OrderItem> OrderItems { get; set; } = new();
}