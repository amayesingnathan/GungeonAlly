CREATE PROCEDURE [dbo].[spInsertSynergy]
	@ID int,
	@Name nvarchar(max),
	@Effect nvarchar(max)
AS
    insert into dbo.Synergies
        (SynergyID, Name, Effect)
    values
        (@ID, @Name, @Effect);   
RETURN 0
