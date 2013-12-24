CREATE TABLE [dbo].[Projects]
(
	[Id] INT NOT NULL IDENTITY, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [Name] NCHAR(10) NOT NULL, 
    [ProjectToken] NVARCHAR(128) NOT NULL, 
    [Description] NVARCHAR(500) NULL, 
    CONSTRAINT [PK_Projects] PRIMARY KEY ([Id]) 
)

GO

CREATE INDEX [IX_Projects_UserID] ON [dbo].[Projects] (UserId)
