CREATE TABLE [dbo].[FBStatus] (
    [year_code]       SMALLINT NULL,
    [week_code]       SMALLINT NULL,
    [year_end_amount] MONEY    NULL,
    [weekly_amount]   MONEY    NULL,
    [donut_cost]      MONEY    NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_FBStatus]
    ON [dbo].[FBStatus]([year_code] ASC);

