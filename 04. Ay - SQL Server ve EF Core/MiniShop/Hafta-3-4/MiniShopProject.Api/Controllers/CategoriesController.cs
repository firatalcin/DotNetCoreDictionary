using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniShopProject.Api.Data;
using MiniShopProject.Api.Dtos;

namespace MiniShopProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly MiniShopDbContext _db;

    public CategoriesController(MiniShopDbContext db)
    {
        _db = db;
    }

    // GET /api/categories/summary
    [HttpGet("summary")]
    public async Task<ActionResult<List<CategorySummaryDto>>> GetSummary(CancellationToken cancellationToken)
    {
        var summary = await _db.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Select(c => new CategorySummaryDto(
                c.Name,
                c.Products.Count(),
                c.Products
                    .OrderByDescending(p => p.Price)
                    .ThenBy(p => p.Id)              // Aynı fiyatta iki ürün olursa hep aynısı seçilsin
                    .Select(p => p.Name)
                    .FirstOrDefault(),
                c.Products
                    .OrderByDescending(p => p.Price)
                    .Select(p => (decimal?)p.Price) // Boş kategoride 0 değil null gelsin
                    .FirstOrDefault()))
            .ToListAsync(cancellationToken);        // SQL sadece burada, tek seferde çalışır

        return Ok(summary);
    }
}