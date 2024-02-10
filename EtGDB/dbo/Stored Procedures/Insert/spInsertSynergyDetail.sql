CREATE PROCEDURE [dbo].[spInsertSynergyDetail]
	@SynergyID int,
	@ItemID int,
	@Type int
AS
    insert into dbo.SynergyDetail
        (SynergyID, ItemID, RequireType)
    values
        (@SynergyID, @ItemID, @Type);  
RETURN 0
