
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[EPM_vw_comments]'))
DROP VIEW [dbo].EPM_vw_comments
GO



CREATE VIEW EPM_vw_comments
AS
with comments as(
select (select ContentTypeId  from Admin.ContentTypes WHERE ContentTypeName = 'Article/News') as ContentTypeId, 'Article'  as ContentTypeName,   articleid as SrcId, blocked, convert(nvarchar(max),comment) as comment, Id, IPAddr, PostBy, PostDate from Article.ArticleComments
union
select (select ContentTypeId  from Admin.ContentTypes WHERE ContentTypeName = 'Forum') as ContentTypeId, 'Forum'  as ContentTypeName,  SrcId, blocked, convert(nvarchar(max),comment) as comment, Id, IPAddr, PostBy, PostDate from forum.forumComments
union
select (select ContentTypeId  from Admin.ContentTypes WHERE ContentTypeName = 'Classified') as ContentTypeId, 'Classified'  as ContentTypeName,  SrcId, blocked, convert(nvarchar(max),comment) as comment, Id, IPAddr, Postby, PostDate from Classified.ClassifiedComments)

select * from comments 
