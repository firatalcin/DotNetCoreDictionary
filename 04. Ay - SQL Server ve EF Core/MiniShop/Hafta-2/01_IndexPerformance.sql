-- =============================================
-- MiniShop - Hafta 2 / Adım 1: Index ve Execution Plan
-- Önce Ctrl+M (Include Actual Execution Plan) aç.
-- Blokları tek tek seçip F5 ile çalıştır, her seferinde "Execution plan" sekmesine bak.
-- =============================================
USE MiniShopDb_Sql;
GO

DROP INDEX IF EXISTS IX_Products_Name ON Products;   -- dosya tekrar çalışabilsin diye
SET STATISTICS IO ON;   -- Messages sekmesinde "logical reads" sayısını da karşılaştır
GO

-- ---------- 1) Index YOK ----------
-- Beklenen plan: Clustered Index Scan (PK__Products__...)  -> tüm tablo okunuyor
SELECT ProductId, Name, Price, Stock
FROM Products
WHERE Name = N'Mekanik Klavye';
GO

-- ---------- 2) INCLUDE'suz index (Key Lookup'ı görmek için) ----------
-- Index sadece Name'i tutar. Price ve Stock için ana tabloya geri dönmek gerekir = Key Lookup.
-- Tablo 10 satır olduğu için optimizer bu index'i kendiliğinden SEÇMEZ (Scan'i daha ucuz bulur),
-- bu yüzden WITH (INDEX(...)) ile zorluyoruz.
-- Beklenen plan: Index Seek (IX_Products_Name) + Key Lookup (Clustered) + Nested Loops
CREATE NONCLUSTERED INDEX IX_Products_Name ON Products(Name);
GO

SELECT ProductId, Name, Price, Stock
FROM Products WITH (INDEX(IX_Products_Name))
WHERE Name = N'Mekanik Klavye';
GO

-- ---------- 3) INCLUDE'lu index (asıl istenen) ----------
-- Price ve Stock index'in yaprak seviyesinde de saklanır -> ana tabloya dönmeye gerek yok.
-- ProductId zaten clustered key olduğu için her non-clustered index'te otomatik bulunur.
-- Beklenen plan: SADECE Index Seek (IX_Products_Name), Key Lookup YOK. Hint'e de gerek yok.
DROP INDEX IX_Products_Name ON Products;
CREATE NONCLUSTERED INDEX IX_Products_Name ON Products(Name) INCLUDE (Price, Stock);
GO

SELECT ProductId, Name, Price, Stock
FROM Products
WHERE Name = N'Mekanik Klavye';
GO

SET STATISTICS IO OFF;
GO
