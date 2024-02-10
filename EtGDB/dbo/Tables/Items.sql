CREATE TABLE [dbo].[Items] (
    [BaseID]     INT            NOT NULL,
    [ItemEffect] NVARCHAR (MAX) NOT NULL,
    [ItemType]   NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED ([BaseID] ASC),
    CONSTRAINT [FK_Items_BaseItems] FOREIGN KEY ([BaseID]) REFERENCES [dbo].[BaseItems] ([BaseID])
);
GO

