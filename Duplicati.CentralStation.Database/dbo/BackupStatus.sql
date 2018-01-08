CREATE TABLE [dbo].[BackupStatus]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Success] BIT NOT NULL, 
    [Message] NVARCHAR(MAX) NOT NULL, 
    [DateStamp] DATETIME2 NOT NULL, 
    [InstanceId] INT NOT NULL
)
