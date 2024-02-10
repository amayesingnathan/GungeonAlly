CREATE TABLE [dbo].[SynergyDetail] (
    [SynergyID]   INT NOT NULL,
    [ItemID]      INT NOT NULL,
    [RequireType] INT NOT NULL,
    CONSTRAINT [FK_SynergyDetail_BaseItems] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[BaseItems] ([BaseID]),
    CONSTRAINT [FK_SynergyDetail_Synergies] FOREIGN KEY ([SynergyID]) REFERENCES [dbo].[Synergies] ([SynergyID])
);
GO

