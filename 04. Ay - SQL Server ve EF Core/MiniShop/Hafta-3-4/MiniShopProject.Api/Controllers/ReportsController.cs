using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniShopProject.Api.Data;
using MiniShopProject.Api.Dtos;

namespace MiniShopProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly MiniShopDbContext _db;

    public ReportsController(MiniShopDbContext db)
    {
        _db = db;
    }

    // GET /api/reports/stock
    [HttpGet("stock")]
    public async Task<ActionResult<List<StockReportDto>>> GetStock(CancellationToken cancellationToken)
    {
        // Raw SQL'de global query filter ÇALIŞMAZ; IsDeleted = 0 koşulu view'ın içinde.
        // Başındaki $: İleride parametre eklersen EF onu SQL parametresine çevirir (SQL injection koruması)
        var report = await _db.Database
            .SqlQuery<StockReportDto>($"SELECT ProductId, Name, Stock, StockStatus FROM vw_StockStatus")
            .OrderBy(r => r.Stock)   // Raw SQL'in üzerine LINQ eklenebilir
            .ToListAsync(cancellationToken);

        return Ok(report);
    }
}
