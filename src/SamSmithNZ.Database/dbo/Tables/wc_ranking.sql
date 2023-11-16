CREATE TABLE [dbo].[wc_ranking] (
    [ranking_date] DATETIME        NOT NULL,
    [team_code]    INT        NOT NULL,
    [ranking]      INT        NOT NULL,
    [score]        DECIMAL (10, 2) NOT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_ranking]
    ON [dbo].[wc_ranking]([team_code] ASC);

