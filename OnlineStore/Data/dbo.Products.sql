CREATE TABLE [dbo].[Products] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Title]              NVARCHAR (MAX) NULL,
    [ProductionDateTime] DATETIME2 (7)  NOT NULL,
    [Price]              FLOAT (53)     NOT NULL,
    [Description]        NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC)
);

