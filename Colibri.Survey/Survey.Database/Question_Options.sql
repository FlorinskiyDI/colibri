CREATE TABLE [dbo].[Question_Options] (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT (NEWSEQUENTIALID()), 
	[Question_Id] UNIQUEIDENTIFIER NOT NULL, 
	[OptionChoise_Id] UNIQUEIDENTIFIER NOT NULL, 

	CONSTRAINT [FK_Question_Options_Questions] FOREIGN KEY ([Question_Id]) REFERENCES [dbo].[Questions]([Id]), 
	CONSTRAINT [FK_Question_Options_OptionChoises] FOREIGN KEY ([OptionChoise_Id]) REFERENCES [dbo].[OptionChoises]([Id]), 
);
