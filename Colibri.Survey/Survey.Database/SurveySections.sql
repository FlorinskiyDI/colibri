CREATE TABLE [dbo].[SurveySections] (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT (NEWSEQUENTIALID()), 
    [Name] NVARCHAR(500) NOT NULL, 
	[IsShowDescription] BIT NULL,
	[IsOpenAccess] BIT NULL,
	[IsLocked] BIT NULL,
    [Description] NVARCHAR(MAX) NULL,
	[IsShowProcessCompletedText] BIT NULL,
	[ProcessCompletedText] NVARCHAR(MAX) NULL,
	[DateCreated] DATETIME NOT NULL,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [FK_SurveySections_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]), 
);
