CREATE PROCEDURE [dbo].[spGetItemById]
	@ItemId int
AS
	SELECT 
         bi.[BaseID]
        ,[Type]
        ,[IconImageData]
        ,[ItemName]
        ,[Quote]
        ,[Quality]
        ,[Description]
        ,[ItemEffect]
        ,[ItemType]
    FROM dbo.BaseItems bi
    INNER JOIN Items i ON bi.BaseID=i.BaseID
    WHERE bi.BaseID=@ItemId
RETURN 0
