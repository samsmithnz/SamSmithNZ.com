CREATE TABLE [dbo].[wc_competition] (
    [competition_code] INT     NOT NULL,
    [competition_name] VARCHAR (50) NOT NULL,
    [region_code]      INT     NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_competition]
    ON [dbo].[wc_competition]([competition_code] ASC);

