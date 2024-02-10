CREATE TABLE [dbo].[Synergies] (
    [SynergyID] INT            NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [Effect]    NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Synergies] PRIMARY KEY CLUSTERED ([SynergyID] ASC)
);
GO

