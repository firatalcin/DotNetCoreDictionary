-- =============================================
-- MiniShop - Hafta 3-4: API için kısıtlı yetkili kullanıcı
-- Şifreyi kendin belirle ve aynısını User Secrets'a yaz. Bu dosyaya gerçek şifreyi yazıp commit'leme!
-- =============================================
USE master;
GO
CREATE LOGIN minishop_app WITH PASSWORD = N'BURAYA_GUCLU_BIR_SIFRE';
GO

USE MiniShopDb_Sql;
GO
CREATE USER minishop_app FOR LOGIN minishop_app;
ALTER ROLE db_datareader ADD MEMBER minishop_app;  -- SELECT (view dahil)
ALTER ROLE db_datawriter ADD MEMBER minishop_app;  -- INSERT, UPDATE, DELETE
-- DDL yetkisi (CREATE / ALTER / DROP) bilerek verilmiyor
GO

-- Test (SSMS'e minishop_app ile bağlanıp çalıştır): yetki hatası vermeli
-- DROP TABLE Products;
