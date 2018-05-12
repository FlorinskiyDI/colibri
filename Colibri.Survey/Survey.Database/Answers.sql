CREATE TABLE [dbo].[Answers] (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT (NEWSEQUENTIALID()), 
	[AnswerNumeric] FLOAT NULL, 
    [AnswerText] NVARCHAR(MAX) NULL, 
	[AnswerBoolean] BIT NOT NULL DEFAULT 0,
	[AnswerDateTime] DATETIME NULL, 
	[User_Id] UNIQUEIDENTIFIER NOT NULL,
	[Question_Option_Id]  UNIQUEIDENTIFIER NOT NULL, 

	CONSTRAINT [FK_Answers_Question_Options] FOREIGN KEY ([Question_Option_Id]) REFERENCES [dbo].[Question_Options]([Id]),
);
