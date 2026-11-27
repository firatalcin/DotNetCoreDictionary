namespace MiniShopProject.Api.Entities;

public class Category : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // 1 kategori -> N ürün
    public List<Product> Products { get; set; } = new();
}