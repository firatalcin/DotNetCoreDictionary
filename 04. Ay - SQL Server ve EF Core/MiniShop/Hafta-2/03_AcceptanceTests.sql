-- =============================================
-- MiniShop - Hafta 2 / Adım 3: Kabul kriterleri
-- Hafta-1 seed verisiyle: ProductId 4 = '27 inç Monitör' (Stock 2), ProductId 5 = 'Clean Code' (Stock 25)
-- =============================================
USE MiniShopDb_Sql;
GO

-- [ ] Stokta 2 adet olan üründen 5 adet -> ROLLBACK, Orders'a kayıt eklenmemeli
DECLARE @OrdersBefore INT = (SELECT COUNT(*) FROM Orders);

BEGIN TRY
    EXEC sp_CreateOrder @CustomerId = 5, @ProductId = 4, @Quantity = 5;
END TRY
BEGIN CATCH
    PRINT N'Beklenen hata: ' + ERROR_MESSAGE();
END CATCH

SELECT @OrdersBefore                          AS OrdersBefore,
       (SELECT COUNT(*) FROM Orders)          AS OrdersAfter,     -- OrdersBefore ile AYNI olmalı
       (SELECT Stock FROM Products WHERE ProductId = 4) AS MonitorStock;   -- hâlâ 2 olmalı
GO

-- [ ] Mutlu yol: Elif 1 adet Clean Code alıyor -> COMMIT
EXEC sp_CreateOrder @CustomerId = 5, @ProductId = 5, @Quantity = 1;

SELECT TOP 1 o.OrderId, o.CustomerId, oi.ProductId, oi.Quantity, oi.UnitPrice
FROM Orders o
INNER JOIN OrderItems oi ON oi.OrderId = o.OrderId
ORDER BY o.OrderId DESC;

SELECT Stock AS CleanCodeStock FROM Products WHERE ProductId = 5;   -- 25 -> 24
-- Not: OrderId'de bir numara atlanmış görebilirsin. IDENTITY değeri ROLLBACK ile geri alınmaz; bu normaldir.
GO
