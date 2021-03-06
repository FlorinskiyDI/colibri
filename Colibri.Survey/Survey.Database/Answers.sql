﻿CREATE TABLE [dbo].[Answers] (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT (NEWSEQUENTIALID()), 
	[AnswerNumeric] FLOAT NULL, 
    [AnswerText] NVARCHAR(MAX) NULL, 
	[AnswerBoolean] BIT NOT NULL DEFAULT 0,
	[AnswerDateTime] DATETIME NULL, 
	[RespondentId] UNIQUEIDENTIFIER NOT NULL,
	[QuestionOptionId]  UNIQUEIDENTIFIER NOT NULL, 

	CONSTRAINT [FK_Answers_Question_Options] FOREIGN KEY ([QuestionOptionId]) REFERENCES [dbo].[Question_Options]([Id]),
	CONSTRAINT [FK_Answers_Respondents] FOREIGN KEY ([RespondentId]) REFERENCES [dbo].[Respondents]([Id]),
);
