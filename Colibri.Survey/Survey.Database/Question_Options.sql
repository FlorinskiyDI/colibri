CREATE TABLE [dbo].[Question_Options] (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT (NEWSEQUENTIALID()), 
	[QuestionId] UNIQUEIDENTIFIER NOT NULL, 
	[OptionChoiseId] UNIQUEIDENTIFIER NOT NULL, 

	CONSTRAINT [FK_Question_Options_Questions] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Questions]([Id]), 
	CONSTRAINT [FK_Question_Options_OptionChoises] FOREIGN KEY ([OptionChoiseId]) REFERENCES [dbo].[OptionChoises]([Id]), 
);
