CREATE PROCEDURE [dbo].[spInsertBaseItem]
	@ID int, 
	@Type int, 
	@Image varbinary(max), 
	@Name nvarchar(max), 
	@Quote nvarchar(max), 
	@Quality int,
	@Description nvarchar(max)
AS
	insert into dbo.BaseItems
		(BaseID, Type, IconImageData, ItemName, Quote, Quality, Description)
	values
		(@ID, @Type, @Image, @Name, @Quote, @Quality, @Description); 
RETURN 0
