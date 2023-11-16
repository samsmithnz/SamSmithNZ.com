CREATE TABLE [dbo].[ff_show] (
    [show_key]      INT      NOT NULL,
    [show_date]     DATETIME      NULL,
    [show_location] VARCHAR (100) NULL,
    [show_city]     VARCHAR (100)  NULL,
    [show_country] VARCHAR(100) NULL, 
    [ffl_code] INT NULL, 
	[ffl_url] VARCHAR(1000) NULL,
    [notes] VARCHAR(4000) NULL, 
    [last_updated] DATETIME NULL, 
    CONSTRAINT [PK_ff_show] PRIMARY KEY CLUSTERED ([show_key] ASC)
);

