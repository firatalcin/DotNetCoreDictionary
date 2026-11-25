namespace MiniShopProject.Api.Dtos;

public record CategorySummaryDto(
    string CategoryName,
    int ProductCount,
    string? MostExpensiveProductName,
    decimal? MostExpensiveProductPrice);