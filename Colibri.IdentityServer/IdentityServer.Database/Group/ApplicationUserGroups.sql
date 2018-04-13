CREATE TABLE [dbo].[ApplicationUserGroups] (

	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT (NEWSEQUENTIALID()),
	[UserId] NVARCHAR (450) NOT NULL,
	[GroupId] UNIQUEIDENTIFIER NOT NULL

    CONSTRAINT [FK_ApplicationUserGroups_ToAspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id]),
    CONSTRAINT [FK_ApplicationUserGroups_ToGroups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups]([Id])
)

GO