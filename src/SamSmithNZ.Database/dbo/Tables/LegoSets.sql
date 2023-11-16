CREATE TABLE [dbo].[LegoSets]
(
	[set_num] VARCHAR(100) NOT NULL PRIMARY KEY,
	[name] VARCHAR(500) NULL,
	[year] INT NULL,
	theme_id INT NULL,	
	num_parts INT NULL
)