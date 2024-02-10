CREATE PROCEDURE [dbo].[spInsertItem]
	@ID int,
    @Effect nvarchar(max),
    @Type nvarchar(max)
AS
	insert into dbo.Items
        (BaseID, ItemEffect, ItemType)
    values
        (@ID, @Effect, @Type); 
RETURN 0
