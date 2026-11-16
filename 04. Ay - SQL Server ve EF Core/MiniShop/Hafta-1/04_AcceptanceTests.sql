-- =============================================
-- MiniShop - Hafta 1 / Adım 5: Kabul kriterleri
-- Her blok HATA vermeli. GO sayesinde biri patlasa da diğeri çalışır.
-- Sonuçları "Messages" sekmesinde gör (hepsi kırmızı olmalı).
-- =============================================
USE MiniShopDb_Sql;
GO

-- [ ] Negatif fiyat -> CHECK constraint hatası (Msg 547, CK_Products_Price)
INSERT INTO Products (CategoryId, Name, Price, Stock) VALUES (1, N'Hatalı Ürün', -10, 5);
GO

-- [ ] Olmayan CategoryId -> FOREIGN KEY hatası (Msg 547, FK_Products_Categories)
INSERT INTO Products (CategoryId, Name, Price, Stock) VALUES (999, N'Kategorisiz Ürün', 10, 5);
GO

-- [ ] (Bonus) Aynı e-posta -> UNIQUE hatası (Msg 2627, UQ_Customers_Email)
INSERT INTO Customers (FirstName, LastName, Email) VALUES (N'Test', N'Kişi', N'ayse@minishop.test');
GO

-- [ ] Yetim tablo kontrolü (diyagrama ek olarak): 4 FK satırı görmelisin
SELECT fk.name AS ForeignKey,
       OBJECT_NAME(fk.parent_object_id)     AS ChildTable,
       OBJECT_NAME(fk.referenced_object_id) AS ParentTable
FROM sys.foreign_keys fk
ORDER BY ChildTable;
