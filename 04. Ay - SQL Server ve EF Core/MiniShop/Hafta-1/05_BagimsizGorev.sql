-- =============================================
-- MiniShop - Hafta 1 / Bağımsız Görev (çözüm sende)
-- Kendi kategorisinin ortalama fiyatından pahalı ürünler.
-- Kolonlar: ProductName, Price, CategoryName, CategoryAvgPrice
--
-- İpuçları:
--   * Önce 03_Reports.sql'deki 3. sorguya benzer şekilde
--     "CategoryId + ortalama fiyat" dönen küçük bir sorgu yaz.
--   * Onu WITH ... AS ( ) ile CTE yap ya da FROM içinde subquery olarak kullan.
--   * Products ve Categories ile JOIN'le, WHERE ile karşılaştır.
--
-- Kendini kontrol et (bu veriyle):
--   Kategori ortalamaları: Elektronik 1700.00, Kitap 240.00, Ev & Yaşam 313.33
--   Beklenen sonuç: 3 satır (her kategoriden 1 ürün). Hangileri? Sen bul :)
-- =============================================
USE MiniShopDb_Sql;
GO

-- Sorgunu buraya yaz

