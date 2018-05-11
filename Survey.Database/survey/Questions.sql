CREATE TABLE [dbo].[Questions] (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT (NEWSEQUENTIALID()), 
	[Parent _Id] UNIQUEIDENTIFIER NULL, 
    [Name] NVARCHAR(250) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
	[Required] BIT NOT NULL DEFAULT 0,
	[OrderNo] INT NOT NULL, 
	[AdditionalAnswer] BIT NOT NULL DEFAULT 0,
	[AllowMultipleOptionAnswers] BIT NOT NULL DEFAULT 0,
	[InputTypes_Id]  UNIQUEIDENTIFIER NOT NULL, 
	[Page_Id]  UNIQUEIDENTIFIER NOT NULL, 
	[OptionGroup_Id]  UNIQUEIDENTIFIER NOT NULL, 


	CONSTRAINT [FK_Questions_InputTypes] FOREIGN KEY ([InputTypes_Id]) REFERENCES [dbo].[InputTypes]([Id]),
	CONSTRAINT [FK_Questions_Pages] FOREIGN KEY ([Page_Id]) REFERENCES [dbo].[Pages]([Id]),
	CONSTRAINT [FK_Questions_OptionGroups] FOREIGN KEY ([OptionGroup_Id]) REFERENCES [dbo].[OptionGroups]([Id]),

	CONSTRAINT [FK_Questions_Questions] FOREIGN KEY ([Parent _Id]) REFERENCES [dbo].[Questions]([Id]),
);
