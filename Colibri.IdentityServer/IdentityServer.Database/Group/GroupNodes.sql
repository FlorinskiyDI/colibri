CREATE TABLE [dbo].[GroupNodes] (

	[AncestorId] UNIQUEIDENTIFIER NOT NULL,
	[OffspringId] UNIQUEIDENTIFIER NOT NULL,
	[Separation]  INT NULL,

	CONSTRAINT [GroupNodes_PK] PRIMARY KEY CLUSTERED (AncestorId ASC, OffspringId ASC),
	CONSTRAINT [FK_GroupNodes_ToAncestor] FOREIGN KEY ([AncestorId]) REFERENCES [dbo].[Groups]([Id]),
	CONSTRAINT [FK_GroupNodes_ToOffspring] FOREIGN KEY ([OffspringId]) REFERENCES [dbo].[Groups]([Id])
)

GO