-- =============================================
-- MiniShop - Hafta 1 / Adım 4: Raporlar
-- =============================================
USE MiniShopDb_Sql;
GO

-- 1) Kategori adıyla ürünler, fiyata göre azalan (INNER JOIN)
SELECT p.Name AS ProductName, c.Name AS CategoryName, p.Price
FROM Products p
INNER JOIN Categories c ON c.CategoryId = p.CategoryId
ORDER BY p.Price DESC;

-- 2) Hiç sipariş vermemiş müşteriler (LEFT JOIN)  -> beklenen: Elif
SELECT cu.CustomerId, cu.FirstName, cu.LastName, cu.Email
FROM Customers cu
LEFT JOIN Orders o ON o.CustomerId = cu.CustomerId
WHERE o.OrderId IS NULL;

-- 3) Kategori bazında ürün sayısı ve ortalama fiyat (GROUP BY)
SELECT c.Name AS CategoryName,
       COUNT(p.ProductId) AS ProductCount,
       AVG(p.Price)       AS AvgPrice
FROM Categories c
LEFT JOIN Products p ON p.CategoryId = c.CategoryId   -- ürünü olmayan kategori de 0 ile görünsün
GROUP BY c.Name;

-- 4) Toplam sipariş tutarı 1000 TL'yi geçen müşteriler (GROUP BY + HAVING) -> beklenen: Zeynep 4500, Ayşe 2130
SELECT cu.FirstName, cu.LastName,
       SUM(oi.Quantity * oi.UnitPrice) AS TotalSpent
FROM Customers cu
INNER JOIN Orders o      ON o.CustomerId = cu.CustomerId
INNER JOIN OrderItems oi ON oi.OrderId   = o.OrderId
GROUP BY cu.CustomerId, cu.FirstName, cu.LastName
HAVING SUM(oi.Quantity * oi.UnitPrice) > 1000
ORDER BY TotalSpent DESC;
GO

-- 5) Stok durumu VIEW'i (CREATE VIEW batch'in ilk komutu olmalı, o yüzden üstte GO var)
CREATE VIEW vw_StockStatus AS
SELECT p.ProductId, p.Name, p.Stock,
       CASE
           WHEN p.Stock = 0 THEN N'Tükendi'
           WHEN p.Stock <= 5 THEN N'Kritik'
           ELSE N'Yeterli'
       END AS StockStatus
FROM Products p;
GO

SELECT * FROM vw_StockStatus ORDER BY Stock;
