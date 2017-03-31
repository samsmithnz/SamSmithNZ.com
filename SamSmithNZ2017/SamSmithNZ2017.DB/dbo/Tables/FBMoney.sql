CREATE TABLE [dbo].[FBMoney] (
    [year_code]   SMALLINT NULL,
    [week_code]   SMALLINT NULL,
    [player_code] SMALLINT NULL,
    [ytd_total]   MONEY    NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_FBMoney]
    ON [dbo].[FBMoney]([year_code] ASC);

