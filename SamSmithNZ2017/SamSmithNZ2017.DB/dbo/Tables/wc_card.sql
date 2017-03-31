CREATE TABLE [dbo].[wc_card] (
    [card_code]      SMALLINT NOT NULL,
    [game_code]      SMALLINT NOT NULL,
    [player_code]    SMALLINT NOT NULL,
    [card_time]      SMALLINT NOT NULL,
    [is_injury_time] BIT      NULL,
    [is_yellow]      BIT      NOT NULL,
    [is_2nd_yellow]  BIT      NOT NULL,
    [is_red]         BIT      NOT NULL
);


GO
CREATE CLUSTERED INDEX [ci_azure_fixup_dbo_wc_card]
    ON [dbo].[wc_card]([card_code] ASC);

