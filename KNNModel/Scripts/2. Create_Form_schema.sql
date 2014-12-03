IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Form].[FormEmail]') AND type in (N'U'))
DROP TABLE [Form].[FormEmail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Form].[Forms]') AND type in (N'U'))
DROP TABLE [Form].[Forms]
GO



IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'Form')
DROP SCHEMA Form
GO


CREATE SCHEMA [Form] AUTHORIZATION [dbo]
GO








-- FormModel.FormEmail
CREATE TABLE [Form].[FormEmail] (
    [email_event_id] int NOT NULL,          -- _emailEventId
    [form_id] int NOT NULL,                 -- _form
    [form_email_id] int IDENTITY NOT NULL,  -- _formEmailId
    [receipients] nvarchar(4000) NOT NULL,  -- _receipients
    CONSTRAINT [pk_FormEmail] PRIMARY KEY ([form_email_id])
)

go

-- FormModel.Form
CREATE TABLE [Form].[Forms] (
    [captcha_fg] tinyint NOT NULL,          -- _captchaFg
    [deleted_fg] int NOT NULL,              -- _deletedFg
    [form_description] nvarchar(255) NULL,  -- _formDescription
    [form_id] int IDENTITY NOT NULL,        -- _formId
    [form_name] nvarchar(120) NOT NULL,     -- _formName
    CONSTRAINT [pk_frms] PRIMARY KEY ([form_id])
)

go

ALTER TABLE [Form].[FormEmail] ADD CONSTRAINT [ref_FormEmail_Forms] FOREIGN KEY ([form_id]) REFERENCES [Form].[Forms]([form_id])

go

-- Index 'idx_form_email_form_id' was not detected in the database. It will be created
CREATE INDEX [idx_form_email_form_id] ON [Form].[FormEmail]([form_id])

go


IF NOT Exists(SELECT 'X' from admin.ContentTypes
where ContentTypeName = 'Form')
BEGIN
	INSERT INTO Admin.ContentTypes(BaseUrl, Enabled,ContentTypeId,ContentTypeName,CategoryFg,UDFAllowedFg)
	VALUES('',1,9,'Form',1,1)
END
ELSE
BEGIN
	UPDATE Admin.ContentTypes
	SET CategoryFg = 1, UDFAllowedFg = 1
	WHERE ContentTypeName = 'Form'
END


GO

-- add column for field _emailUDFInfoId
ALTER TABLE [Form].[FormEmail] ADD [email_UDF_info_id] int NULL

go

-- Column was read from database as: [receipients] nvarchar(4000) not null
-- modify column for field _receipients
ALTER TABLE [Form].[FormEmail] ALTER COLUMN [receipients] nvarchar(4000) NULL

go

ALTER TABLE [Form].[FormEmail] ADD CONSTRAINT [ref_FormEmail_EmailEvents] FOREIGN KEY ([email_event_id]) REFERENCES [Admin].[EmailEvents]([email_event_id])

go

-- Index 'idx_FormEmail_email_event_id' was not detected in the database. It will be created
CREATE INDEX [idx_FormEmail_email_event_id] ON [Form].[FormEmail]([email_event_id])

go

