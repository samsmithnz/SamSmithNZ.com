CREATE TABLE [dbo].[fb_odds] (
    [team_name]        VARCHAR (100)   NOT NULL,
    [odds_date]        DATETIME        NOT NULL,
    [odds_probability] DECIMAL (18, 4) NOT NULL,
    [odds_mean]        DECIMAL (18, 4) NOT NULL,
    [odds_max]         DECIMAL (18, 4) NOT NULL,
    [odds_min]         DECIMAL (18, 4) NOT NULL,
    [odds_stdDev]      DECIMAL (18, 4) NOT NULL,
    [odds_sample_size] INT             NOT NULL,
    [year_code]        INT             NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_fb_odds]
    ON [dbo].[fb_odds]([odds_sample_size] ASC);

