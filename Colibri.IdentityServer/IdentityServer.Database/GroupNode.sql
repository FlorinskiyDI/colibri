CREATE TABLE [dbo].[GroupNode] (
    [AncestorId]  UNIQUEIDENTIFIER NOT NULL,
    [OffspringId] UNIQUEIDENTIFIER NOT NULL,
    [Depth]       INT              NOT NULL,
    CONSTRAINT [PK_GroupNode] PRIMARY KEY CLUSTERED ([AncestorId] ASC, [OffspringId] ASC),
    CONSTRAINT [FK_Ancestor_ToOffspring] FOREIGN KEY ([OffspringId]) REFERENCES [dbo].[Groups] ([Id]),
    CONSTRAINT [FK_Offspring_ToAncestor] FOREIGN KEY ([AncestorId]) REFERENCES [dbo].[Groups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_GroupNode_OffspringId]
    ON [dbo].[GroupNode]([OffspringId] ASC);

