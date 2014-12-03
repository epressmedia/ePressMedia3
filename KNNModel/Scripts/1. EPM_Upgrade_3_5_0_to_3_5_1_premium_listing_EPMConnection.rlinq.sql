-- add column for field _premiumListing
ALTER TABLE [Biz].[BusinessEntity] ADD [premium_listing] tinyint NULL

go

UPDATE [Biz].[BusinessEntity] SET [premium_listing] = 0

go

ALTER TABLE [Biz].[BusinessEntity] ALTER COLUMN [premium_listing] tinyint NOT NULL

go

