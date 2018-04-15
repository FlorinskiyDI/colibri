CREATE TABLE [dbo].[ApplicationUserGroups] (
    [Id]      UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [GroupId] UNIQUEIDENTIFIER NOT NULL,
    [UserId]  NVARCHAR (450)   NOT NULL,
    CONSTRAINT [PK_ApplicationUserGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApplicationUserGroups_ToAspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_ApplicationUserGroups_ToGroups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationUserGroups_GroupId]
    ON [dbo].[ApplicationUserGroups]([GroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationUserGroups_UserId]
    ON [dbo].[ApplicationUserGroups]([UserId] ASC);

