using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniShopProject.Api.Data;
using MiniShopProject.Api.Dtos;
using MiniShopProject.Api.Entities;

namespace MiniShopProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly MiniShopDbContext _db;

    public ProductsController(MiniShopDbContext db)
    {
        _db = db;
    }
    
    // GET /api/products?page=1&pageSize=10&categoryId=1
    [HttpGet]
    public async Task<ActionResult<List<ProductListDto>>> GetAll(
        int page = 1,
        int pageSize = 10,
        int? categoryId = null,
        CancellationToken cancellationToken = default)
    {
        if (page < 1 || pageSize < 1 || pageSize > 100)
            return BadRequest("page en az 1, pageSize 1 ile 100 arasında olmalı.");

        // Henüz SQL çalışmadı; sadece sorguyu adım adım kuruyoruz (IQueryable)
        var query = _db.Products.AsNoTracking();

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        var products = await query
            .OrderBy(p => p.Id) // Sayfalamada sabit bir sıralama şart
            .Select(p => new ProductListDto(p.Id, p.Name, p.Price, p.Category.Name))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken); // SQL bu satırda çalışır

        return Ok(products);
    }
    
    // POST /api/products
    [HttpPost]
    public async Task<ActionResult<ProductListDto>> Create(
        ProductRequest request,
        CancellationToken cancellationToken)
    {
        // Kategori var mı? Varsa adını da al (cevaptaki DTO için lazım)
        var categoryName = await _db.Categories
            .Where(c => c.Id == request.CategoryId)
            .Select(c => c.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (categoryName is null)
            return BadRequest($"{request.CategoryId} id'li kategori bulunamadı.");

        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock,
            CategoryId = request.CategoryId
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync(cancellationToken); // INSERT burada; product.Id artık dolu

        var dto = new ProductListDto(product.Id, product.Name, product.Price, categoryName);
        return Created($"/api/products/{product.Id}", dto); // 201 Created
    }
    
    // PUT /api/products/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        ProductRequest request,
        CancellationToken cancellationToken)
    {
        var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (product is null)
            return NotFound();

        var categoryExists = await _db.Categories.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
        if (!categoryExists)
            return BadRequest($"{request.CategoryId} id'li kategori bulunamadı.");

        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.CategoryId = request.CategoryId;

        try
        {
            await _db.SaveChangesAsync(cancellationToken); // Sadece değişen kolonlar için UPDATE
        }
        catch (DbUpdateConcurrencyException)
        {
            // Biz okuduktan sonra başka biri bu satırı değiştirdi (RowVersion tutmadı)
            return Conflict("Bu ürün siz düzenlerken başka biri tarafından değiştirildi. Tekrar deneyin.");
        }

        return NoContent(); // 204
    }

    // DELETE /api/products/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (product is null)
            return NotFound();

        _db.Products.Remove(product);                  // Görünüşte silme...
        await _db.SaveChangesAsync(cancellationToken); // ...ama override sayesinde UPDATE IsDeleted = 1
        return NoContent(); // 204
    }
}