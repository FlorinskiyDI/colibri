CREATE TABLE [dbo].[Questions] (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT (NEWSEQUENTIALID()), 
	[ParentId] UNIQUEIDENTIFIER NULL, 
    [Name] NVARCHAR(250) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
	[Required] BIT NOT NULL DEFAULT 0,
	[OrderNo] INT NOT NULL, 
	[AdditionalAnswer] BIT NOT NULL DEFAULT 0,
	[AllowMultipleOptionAnswers] BIT NOT NULL DEFAULT 0,
	[InputTypesId]  UNIQUEIDENTIFIER NOT NULL, 
	[PageId]  UNIQUEIDENTIFIER NOT NULL, 
	[OptionGroupId]  UNIQUEIDENTIFIER NOT NULL, 


	CONSTRAINT [FK_Questions_InputTypes] FOREIGN KEY ([InputTypesId]) REFERENCES [dbo].[InputTypes]([Id]),
	CONSTRAINT [FK_Questions_Pages] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Pages]([Id]),
	CONSTRAINT [FK_Questions_OptionGroups] FOREIGN KEY ([OptionGroupId]) REFERENCES [dbo].[OptionGroups]([Id]),

	CONSTRAINT [FK_Questions_Questions] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Questions]([Id]),
);
