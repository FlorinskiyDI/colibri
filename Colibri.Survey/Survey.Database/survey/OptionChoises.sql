CREATE TABLE [dbo].[OptionChoises] (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT (NEWSEQUENTIALID()), 
    [Name] NVARCHAR(250) NOT NULL, 
	[OptionGroup_Id] UNIQUEIDENTIFIER NOT NULL, 
	CONSTRAINT [FK_OptionChoises_OptionGroups] FOREIGN KEY ([OptionGroup_Id]) REFERENCES [dbo].[OptionGroups]([Id])
);
