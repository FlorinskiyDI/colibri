CREATE TABLE [dbo].[Groups] (
    [Id]       UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Name]     NVARCHAR (100)   NOT NULL,
    [ParentId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Groups_ToGroups] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Groups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Groups_ParentId]
    ON [dbo].[Groups]([ParentId] ASC);

