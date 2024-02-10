CREATE PROCEDURE [dbo].[spGetGunByName]
	@GunName nvarchar(max)
AS
	SELECT 
         bi.[BaseID]
        ,[Type]
        ,[IconImageData]
        ,[ItemName]
        ,[Quote]
        ,[Quality]
        ,[Description]
        ,[Notes]
        ,[GunType]
        ,[DPS]
        ,[MagSize]
        ,[AmmoCap]
        ,[Damage]
        ,[FireRate]
        ,[ReloadTime]
        ,[ShotSpeed]
        ,[Range]
        ,[Force]
        ,[Spread]
        ,[Class]
    FROM dbo.BaseItems bi
    INNER JOIN Guns g ON bi.BaseID=g.BaseID
    WHERE bi.ItemName=@GunName
RETURN 0
