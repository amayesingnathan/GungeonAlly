CREATE TABLE [dbo].[Guns] (
    [BaseID]     INT            NOT NULL,
    [Notes]      NVARCHAR (MAX) NOT NULL,
    [GunType]    NVARCHAR (MAX) NOT NULL,
    [DPS]        NVARCHAR (MAX) NOT NULL,
    [MagSize]    NVARCHAR (MAX) NOT NULL,
    [AmmoCap]    NVARCHAR (MAX) NOT NULL,
    [Damage]     NVARCHAR (MAX) NOT NULL,
    [FireRate]   NVARCHAR (MAX) NOT NULL,
    [ReloadTime] NVARCHAR (MAX) NOT NULL,
    [ShotSpeed]  NVARCHAR (MAX) NOT NULL,
    [Range]      NVARCHAR (MAX) NOT NULL,
    [Force]      NVARCHAR (MAX) NOT NULL,
    [Spread]     NVARCHAR (MAX) NOT NULL,
    [Class]      NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Guns] PRIMARY KEY CLUSTERED ([BaseID] ASC),
    CONSTRAINT [FK_Guns_BaseItems] FOREIGN KEY ([BaseID]) REFERENCES [dbo].[BaseItems] ([BaseID])
);
GO

