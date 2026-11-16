-- =============================================
-- MiniShop - Hafta 1 / Adım 3: Örnek veriler
-- Not: Boş veritabanında çalıştır; IDENTITY değerleri 1'den başlar varsayılıyor.
-- =============================================
USE MiniShopDb_Sql;
GO

INSERT INTO Categories (Name) VALUES
(N'Elektronik'),      -- 1
(N'Kitap'),           -- 2
(N'Ev & Yaşam');      -- 3

-- Stoklar bilerek karışık: 0 (Tükendi), 1-5 (Kritik), >5 (Yeterli)
INSERT INTO Products (CategoryId, Name, Price, Stock) VALUES
(1, N'Kablosuz Kulaklık', 750.00, 12),   -- 1
(1, N'Mekanik Klavye',   1200.00,  4),   -- 2
(1, N'Oyuncu Mouse',      350.00,  0),   -- 3
(1, N'27 inç Monitör',   4500.00,  2),   -- 4
(2, N'Clean Code',        450.00, 25),   -- 5
(2, N'Suç ve Ceza',       120.00,  0),   -- 6
(2, N'Sefiller',          150.00,  8),   -- 7
(3, N'Seramik Kupa',       90.00, 40),   -- 8
(3, N'Masa Lambası',      650.00,  5),   -- 9
(3, N'Yastık',            200.00,  1);   -- 10

INSERT INTO Customers (FirstName, LastName, Email) VALUES
(N'Ayşe',   N'Yılmaz', N'ayse@minishop.test'),    -- 1
(N'Mehmet', N'Demir',  N'mehmet@minishop.test'),  -- 2
(N'Zeynep', N'Kaya',   N'zeynep@minishop.test'),  -- 3
(N'Can',    N'Öztürk', N'can@minishop.test'),     -- 4
(N'Elif',   N'Şahin',  N'elif@minishop.test');    -- 5 -> hiç sipariş vermeyecek (LEFT JOIN testi)

INSERT INTO Orders (CustomerId, OrderDate) VALUES
(1, '2026-11-01'),   -- 1 Ayşe
(2, '2026-11-02'),   -- 2 Mehmet
(1, '2026-11-05'),   -- 3 Ayşe
(3, '2026-11-06'),   -- 4 Zeynep
(4, '2026-11-08');   -- 5 Can

INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) VALUES
(1, 2, 1, 1200.00),  -- Ayşe:   Klavye
(1, 8, 2,   90.00),  --         2 Kupa          -> sipariş 1 = 1380
(2, 5, 1,  450.00),  -- Mehmet: Clean Code
(2, 6, 1,  120.00),  --         Suç ve Ceza     -> sipariş 2 = 570
(3, 1, 1,  750.00),  -- Ayşe:   Kulaklık        -> sipariş 3 = 750
(4, 4, 1, 4500.00),  -- Zeynep: Monitör         -> sipariş 4 = 4500
(5, 9, 1,  650.00);  -- Can:    Masa Lambası    -> sipariş 5 = 650

-- Beklenen müşteri toplamları: Ayşe 2130, Zeynep 4500, Can 650, Mehmet 570, Elif 0
GO
