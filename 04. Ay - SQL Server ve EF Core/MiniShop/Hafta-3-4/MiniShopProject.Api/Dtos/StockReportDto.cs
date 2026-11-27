namespace MiniShopProject.Api.Dtos;

// Property adları vw_StockStatus kolon adlarıyla birebir aynı olmalı (EF isimden eşleştirir)
public class StockReportDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string StockStatus { get; set; } = string.Empty;
}
