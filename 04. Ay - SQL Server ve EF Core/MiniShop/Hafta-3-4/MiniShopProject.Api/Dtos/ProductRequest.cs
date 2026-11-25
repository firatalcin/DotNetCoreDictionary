using System.ComponentModel.DataAnnotations;

namespace MiniShopProject.Api.Dtos;

public class ProductRequest
{
    [Required, StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "1000000")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}