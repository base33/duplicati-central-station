CREATE TABLE [dbo].[BackupReport]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Success] BIT NOT NULL, 
    [Message] NVARCHAR(MAX) NOT NULL, 
    [InstanceId] INT NOT NULL, 
    [BeginDate] DATETIME2 NOT NULL, 
    [EndDate] DATETIME2 NOT NULL, 
    [AddedFiles] INT NOT NULL, 
    [SizeOfAddedFiles] INT NOT NULL, 
    [ExaminedFiles] INT NOT NULL, 
    [SizeOfExaminedFiles] INT NOT NULL, 
    [NotProcessedFiles] INT NOT NULL, 
    [Duration] TIME NOT NULL, 
    [DeletedFiles] INT NOT NULL, 
    CONSTRAINT [FK_BackupReport_ToInstance] FOREIGN KEY ([InstanceId]) REFERENCES [Instance]([Id])
)
