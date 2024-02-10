CREATE PROCEDURE [dbo].[spGetItemByName]
	@ItemName nvarchar(max)
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
    WHERE bi.ItemName=@ItemName
RETURN 0
