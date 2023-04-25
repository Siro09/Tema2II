Create table [dbo].[Credentials]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] TEXT NOT NULL , 
    [Password] TEXT NOT NULL 
)
