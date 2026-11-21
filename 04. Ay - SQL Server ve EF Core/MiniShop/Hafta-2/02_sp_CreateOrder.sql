-- =============================================
-- MiniShop - Hafta 2 / Adım 2: Sipariş oluşturan Stored Procedure
-- Tek ürünlü sipariş: müşteri + ürün + adet.
-- =============================================
USE MiniShopDb_Sql;
GO

CREATE OR ALTER PROCEDURE sp_CreateOrder
    @CustomerId INT,
    @ProductId  INT,
    @Quantity   INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;   -- beklenmedik bir hatada da transaction yarım kalmasın, tamamen geri alınsın

    BEGIN TRY
        BEGIN TRAN;

        DECLARE @UnitPrice DECIMAL(18,2), @OrderId INT;

        SELECT @UnitPrice = Price FROM Products WHERE ProductId = @ProductId;
        IF @UnitPrice IS NULL
            THROW 50001, N'Ürün bulunamadı.', 1;

        -- 1) Önce sipariş başlığı ve kalemi
        INSERT INTO Orders (CustomerId) VALUES (@CustomerId);    -- müşteri yoksa FK hatası -> CATCH
        SET @OrderId = SCOPE_IDENTITY();

        INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice)
        VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice);    -- adet <= 0 ise CHECK hatası -> CATCH

        -- 2) Sonra stok düş.
        -- "AND Stock >= @Quantity" kontrolü UPDATE'in içinde: kontrol ve düşme tek adımda olur,
        -- aynı anda gelen iki sipariş aynı stoğu ikisi birden düşemez.
        UPDATE Products
        SET Stock = Stock - @Quantity
        WHERE ProductId = @ProductId AND Stock >= @Quantity;

        IF @@ROWCOUNT = 0
            THROW 50002, N'Yetersiz stok. Sipariş iptal edildi.', 1;
        -- (Bu kontrol olmasaydı bile CK_Products_Stock (Stock >= 0) patlar ve yine CATCH'e düşerdik.)

        COMMIT TRAN;
        SELECT @OrderId AS NewOrderId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;   -- Orders ve OrderItems insert'leri de geri alınır

        THROW;   -- orijinal hata mesajını çağırana ilet
    END CATCH
END
GO
