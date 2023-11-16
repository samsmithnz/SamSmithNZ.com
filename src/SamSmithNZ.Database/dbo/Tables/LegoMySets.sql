CREATE TABLE [dbo].[LegoMySets]
(
	[set_num] VARCHAR(100) NOT NULL PRIMARY KEY, 
	[owner_code] INT NOT NULL,
    [owned] BIT NOT NULL, 
    [wanted] BIT NOT NULL
)