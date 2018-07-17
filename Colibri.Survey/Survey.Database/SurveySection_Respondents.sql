CREATE TABLE [dbo].[SurveySectoin_Respondents] (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT (NEWSEQUENTIALID()), 
    [RespondentId] UNIQUEIDENTIFIER NOT NULL,
	[SurveySectionId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [FK_SurveySectoin_Respondents_Respondents] FOREIGN KEY ([RespondentId]) REFERENCES [dbo].[Respondents]([Id]),
	CONSTRAINT [FK_SurveySectoin_Respondents_SurveySection] FOREIGN KEY ([SurveySectionId]) REFERENCES [dbo].[SurveySections]([Id]),
);
