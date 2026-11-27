using System.ComponentModel.DataAnnotations;

namespace MiniShopProject.Api.Dtos;

public class CreateOrderRequest
{
    [Range(1, int.MaxValue)]
    public int CustomerId { get; set; }

    [Required, MinLength(1)]   // En az 1 ürün olmalı
    public List<OrderItemRequest> Items { get; set; } = new();
}

public class OrderItemRequest
{
    [Range(1, int.MaxValue)]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
