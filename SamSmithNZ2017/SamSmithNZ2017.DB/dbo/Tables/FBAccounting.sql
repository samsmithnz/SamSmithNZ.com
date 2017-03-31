CREATE TABLE [dbo].[FBAccounting] (
    [year_code]   SMALLINT     NULL,
    [player_code] SMALLINT     NULL,
    [week_code]   SMALLINT     NULL,
    [amount_paid] DECIMAL (18) NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_FBAccounting]
    ON [dbo].[FBAccounting]([year_code] ASC);

