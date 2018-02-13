CREATE TABLE [dbo].[Instance]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(250) NOT NULL, 
    [Url] NVARCHAR(250) NOT NULL, 
    [HoursBetweenBackups] INT NOT NULL, 
    [DateTimeFormat] NVARCHAR(50) NULL
)
