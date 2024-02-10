CREATE TABLE [dbo].[BaseItems] (
    [BaseID]        INT             NOT NULL,
    [Type]          INT             NOT NULL,
    [IconImageData] VARBINARY (MAX) NOT NULL,
    [ItemName]      NVARCHAR (MAX)  NOT NULL,
    [Quote]         NVARCHAR (MAX)  NOT NULL,
    [Quality]       INT             NOT NULL,
    [Description]   NVARCHAR (MAX)  NOT NULL,
    CONSTRAINT [PK_BaseItems] PRIMARY KEY CLUSTERED ([BaseID] ASC)
);
GO

