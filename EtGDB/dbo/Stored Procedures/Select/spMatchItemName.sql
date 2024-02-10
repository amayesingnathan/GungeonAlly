CREATE PROCEDURE [dbo].[spMatchItemName]
	@Name nvarchar(max)
AS
	declare @SearchString nvarchar(max) = '%' + @Name + '%';
    declare @GoodMatch    nvarchar(max) = '%' + @Name;
    declare @BadMatch     nvarchar(max) = @Name + '%';

    select TOP 50 * from dbo.BaseItems
    where ItemName like @SearchString
    ORDER BY 
        CASE
            WHEN ItemName LIKE @GoodMatch THEN 1
            WHEN ItemName LIKE @BadMatch THEN 3
            ELSE 2
        END, ItemName
RETURN 0
