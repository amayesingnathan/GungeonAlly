CREATE PROCEDURE [dbo].[spResetAllTables]
AS
	truncate table dbo.Items;
    truncate table dbo.Guns;
    truncate table dbo.SynergyDetail;
    delete from dbo.Synergies;
    delete from dbo.BaseItems;
RETURN 0
