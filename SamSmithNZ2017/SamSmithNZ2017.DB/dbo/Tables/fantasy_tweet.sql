CREATE TABLE [dbo].[fantasy_tweet] (
    [tweet_date] DATETIME      NOT NULL,
    [word]       VARCHAR (140) NOT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_fantasy_tweet]
    ON [dbo].[fantasy_tweet]([tweet_date] ASC);

