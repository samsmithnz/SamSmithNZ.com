CREATE TABLE [dbo].[wc_odds] (
    [team_name]        VARCHAR (100)   NOT NULL,
    [odds_date]        DATETIME        NOT NULL,
    [odds_probability] DECIMAL (18, 4) NOT NULL,
    [odds_mean]        DECIMAL (18, 4) NOT NULL,
    [odds_max]         DECIMAL (18, 4) NOT NULL,
    [odds_min]         DECIMAL (18, 4) NOT NULL,
    [odds_stdDev]      DECIMAL (18, 4) NOT NULL,
    [odds_sample_size] INT             NOT NULL,
    [tournament_code]  SMALLINT        NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_odds]
    ON [dbo].[wc_odds]([odds_sample_size] ASC);

