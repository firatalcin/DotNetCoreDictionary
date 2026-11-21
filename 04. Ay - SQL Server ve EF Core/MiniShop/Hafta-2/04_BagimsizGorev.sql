-- =============================================
-- MiniShop - Hafta 2 / Bağımsız Görev (çözüm sende)
-- Belirli bir müşterinin, belirli tarih aralığındaki siparişleri.
-- Sorgu SARGable olsun + onu en hızlı yapacak Composite Index'i oluştur + Execution Plan'da kanıtla.
--
-- İpuçları:
--   * SARGable = SQL Server'ın index'te "arama" (Seek) yapabileceği WHERE.
--     WHERE'de kolona fonksiyon uygularsan (YEAR(), CAST(), CONVERT(), DATEADD(kolon)...) index'i kullanamaz -> Scan.
--     Fonksiyonu kolona değil, karşılaştırdığın değere uygula.
--   * Tarih aralığında BETWEEN '...' AND '...' yerine  >= başlangıç  AND  < bitişten sonraki gün
--     kalıbını düşün (DATETIME2'de saat kısmı var, BETWEEN son günün siparişlerini kaçırabilir).
--   * Composite index'te kolon sırası önemli: önce EŞİTLİK (=) aranan kolon, sonra ARALIK (>=, <) aranan kolon.
--   * Index adı önerisi: IX_Orders_CustomerId_OrderDate
--
-- Kanıt (Ctrl+M açık):
--   Index'ten ÖNCE: Clustered Index Scan
--   Index'ten SONRA: Index Seek (senin index'in), Key Lookup YOK
--   Index Seek ikonuna sağ tık -> Properties: "Seek Predicates" içinde HEM CustomerId HEM OrderDate görünmeli.
--   Bonus: Aynı sorguyu bilerek YEAR(OrderDate) = 2026 ile yaz ve Properties'e tekrar bak:
--          Seek Predicates'te sadece CustomerId kalır, tarih "Predicate"e düşer (satır satır kontrol = SARGable değil).
--
-- Kendini kontrol et: CustomerId = 1, 2026-11-01 ile 2026-11-05 arası (iki gün dahil) -> 2 sipariş (OrderId 1 ve 3)
-- =============================================
USE MiniShopDb_Sql;
GO

-- Sorgunu ve index'ini buraya yaz

