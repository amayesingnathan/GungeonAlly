CREATE PROCEDURE [dbo].[spInsertGun]
	@ID int,
	@Notes nvarchar(max),
	@GunType nvarchar(max),
	@DPS nvarchar(max),
	@MagSize nvarchar(max),
	@AmmoCap nvarchar(max),
	@Damage nvarchar(max),
	@FireRate nvarchar(max),
	@ReloadTime nvarchar(max),
	@ShotSpeed nvarchar(max),
	@Range nvarchar(max),
	@Force nvarchar(max),
	@Spread nvarchar(max),
	@Class nvarchar(max)

AS
    insert into dbo.Guns
        (BaseID, Notes, GunType, DPS, MagSize, AmmoCap, Damage, FireRate, ReloadTime, ShotSpeed, Range, Force, Spread, Class)
    values
        (@ID, @Notes, @GunType, @DPS, @MagSize, @AmmoCap, @Damage, @FireRate, @ReloadTime, @ShotSpeed, @Range, @Force, @Spread, @Class);    
RETURN 0
