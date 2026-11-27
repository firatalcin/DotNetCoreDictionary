using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniShopProject.Api.Data;
using MiniShopProject.Api.Dtos;
using MiniShopProject.Api.Entities;

namespace MiniShopProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly MiniShopDbContext _db;

    public OrdersController(MiniShopDbContext db)
    {
        _db = db;
    }

    // POST /api/orders
    [HttpPost]
    public async Task<ActionResult<OrderCreatedDto>> Create(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        // OrderItems tablosunun anahtarı (OrderId, ProductId); aynı ürün iki satırda olursa PK hatası alırız
        var productIds = request.Items.Select(i => i.ProductId).ToList();
        if (productIds.Distinct().Count() != productIds.Count)
            return BadRequest("Aynı ürün birden fazla satırda olamaz, adedini artırın.");

        var customerExists = await _db.Customers.AnyAsync(c => c.Id == request.CustomerId, cancellationToken);
        if (!customerExists)
            return BadRequest($"{request.CustomerId} id'li müşteri bulunamadı.");

        // AsNoTracking KULLANMIYORUZ: Stoğu değiştireceğimiz için EF'in bu ürünleri takip etmesi lazım.
        // Ürünler okunurken RowVersion değerleri de hafızaya alınıyor.
        var products = await _db.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        if (products.Count != productIds.Count)
            return BadRequest("Siparişteki ürünlerden en az biri bulunamadı.");

        var order = new Order { CustomerId = request.CustomerId };

        foreach (var item in request.Items)
        {
            var product = products.First(p => p.Id == item.ProductId);

            if (product.Stock < item.Quantity)
                return BadRequest($"'{product.Name}' için yeterli stok yok. Mevcut stok: {product.Stock}");

            product.Stock -= item.Quantity;

            order.OrderItems.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price   // Sipariş anındaki fiyatı saklıyoruz
            });
        }

        _db.Orders.Add(order);

        try
        {
            // Tek transaction: INSERT Orders + INSERT OrderItems + UPDATE Products ... WHERE RowVersion = @eski
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Siz sipariş verirken ürünlerden birinin stoğu başka biri tarafından değiştirildi. Lütfen siparişi tekrar deneyin.");
        }

        var total = order.OrderItems.Sum(i => i.Quantity * i.UnitPrice);
        return Created($"/api/orders/{order.Id}", new OrderCreatedDto(order.Id, total)); // 201
    }
}
