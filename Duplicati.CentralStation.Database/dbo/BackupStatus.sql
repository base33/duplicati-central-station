CREATE TABLE [dbo].[BackupStatus]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Success] BIT NOT NULL, 
    [Message] NVARCHAR(MAX) NOT NULL, 
    [InstanceId] INT NOT NULL, 
    [BeginDate] DATETIME2 NOT NULL, 
    [EndDate] DATETIME2 NOT NULL
)
