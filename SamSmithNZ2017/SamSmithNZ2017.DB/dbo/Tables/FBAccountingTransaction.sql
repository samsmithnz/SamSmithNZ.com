CREATE TABLE [dbo].[FBAccountingTransaction] (
    [year_code]             SMALLINT NULL,
    [week_code]             SMALLINT NULL,
    [player_code]           SMALLINT NULL,
    [amount]                MONEY    NULL,
    [transaction_type_code] SMALLINT NULL,
    [last_updated]          DATETIME NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_FBAccountingTransaction]
    ON [dbo].[FBAccountingTransaction]([year_code] ASC);

