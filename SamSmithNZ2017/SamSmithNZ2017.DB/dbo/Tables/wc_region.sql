CREATE TABLE [dbo].[wc_region] (
    [region_code]   INT     NOT NULL,
    [region_abbrev] VARCHAR (50) NOT NULL,
    [region_name]   VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_wc_region] PRIMARY KEY CLUSTERED ([region_code] ASC)
);

