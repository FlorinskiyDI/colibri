CREATE TABLE [dbo].[MemberGroups] (
    [Id]      UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [GroupId] UNIQUEIDENTIFIER NOT NULL,
    [UserId]  NVARCHAR (450)   NOT NULL,	
    [DateOfSubscribe] DATETIMEOFFSET (7) NOT NULL,
    CONSTRAINT [PK_MemberGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MemberGroups_ToAspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_MemberGroups_ToGroups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_MemberGroups_GroupId]
    ON [dbo].[MemberGroups]([GroupId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_MemberGroups_UserId]
    ON [dbo].[MemberGroups]([UserId] ASC);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MemberGroups_UserId_GroupId]
    ON [dbo].[MemberGroups]([UserId] ASC, [GroupId] ASC);

