-- =============================================
-- MiniShop - Hafta 3-4: Stok durumu VIEW'i (EF şemasına göre)
-- Hafta-1'deki view eski şemaya göreydi (p.ProductId); EF tablosunda kolon adı Id.
-- Raw SQL'de EF'in global query filter'ı çalışmaz, bu yüzden IsDeleted = 0 burada.
-- =============================================
USE MiniShopDb_Sql;
GO

CREATE OR ALTER VIEW vw_StockStatus AS
SELECT p.Id AS ProductId, p.Name, p.Stock,
       CASE
           WHEN p.Stock = 0 THEN N'Tükendi'
           WHEN p.Stock <= 5 THEN N'Kritik'
           ELSE N'Yeterli'
       END AS StockStatus
FROM Products p
WHERE p.IsDeleted = 0;
GO
