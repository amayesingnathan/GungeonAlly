CREATE PROCEDURE [dbo].[spGetSynergies]
	@ItemId int
AS
	select
        s.SynergyID, 
	    s.Name, 
	    s.Effect, 
	    sd.RequireType, 
	    bi.BaseID,
	    bi.Type,
	    bi.IconImageData,
	    bi.ItemName,
	    bi.Quote,
	    bi.Quality,
        bi.Description
    from
	    dbo.SynergyDetail sd
	    inner join dbo.Synergies s on s.SynergyID=sd.SynergyID		
	    inner join dbo.SynergyDetail osd on s.SynergyID=osd.SynergyID
	    inner join dbo.BaseItems bi on bi.BaseID=osd.ItemID
	    where sd.ItemID=@ItemId
RETURN 0
