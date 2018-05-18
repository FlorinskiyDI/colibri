﻿CREATE TABLE [dbo].[OptionChoises] (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT (NEWSEQUENTIALID()), 
    [Name] NVARCHAR(250) NOT NULL, 
	[OptionGroupId] UNIQUEIDENTIFIER NOT NULL, 
	[OrderNo] INT NOT NULL,
	CONSTRAINT [FK_OptionChoises_OptionGroups] FOREIGN KEY ([OptionGroupId]) REFERENCES [dbo].[OptionGroups]([Id])
);
